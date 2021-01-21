using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Repositorio
    {
        private string conString = "Data Source=localhost;Initial Catalog=ControleAcesso;Integrated Security=SSPI;";
        private SqlConnection conexao;

        public Repositorio()
        {
            conexao = new SqlConnection(conString);
            conexao.Open();
        }

        public List<Ambiente> CarregarAmbientes()
        {
            List<Ambiente> listaAmbientes = new List<Ambiente>();

            using (SqlCommand query = new SqlCommand(@"SELECT ID, 
                                                              NOME
                                                      FROM AMBIENTES ", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listaAmbientes.Add(new Ambiente(rd.GetInt32(0), rd.GetString(1), new Queue<Log>()));
                    }
                }
            }

            foreach (var r in listaAmbientes)
            {
                foreach (var x in BuscarLogsPorAmbiente(r.Id))
                {
                    r.Logs.Enqueue(x);
                }
            }

            return listaAmbientes;
        }

        private List<Log> BuscarLogsPorAmbiente(int ambienteId)
        {
            List<Log> listaLogs = new List<Log>();

            using (SqlCommand query = new SqlCommand($@"SELECT *
                                                      FROM LOGS WHERE ID_AMBIENTES = {ambienteId.ToString()}", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listaLogs.Add(new Log(rd.GetInt32(0), rd.GetDateTime(1), null, rd.GetByte(2) == 1 ? true : false));
                    }
                }

            }

            foreach (var lg in listaLogs)
            {
                lg.Usuario = BuscaUsuarioPorLog(lg.Id);
            }

            return listaLogs;

        }

        private Usuario BuscaUsuarioPorLog(int logId)
        {
            Usuario usuario = null;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 US.ID, US.NOME
                                                      FROM USUARIOS US JOIN LOGS LG ON US.ID = LG.ID_USUARIOS 
                                                      WHERE LG.ID = {logId.ToString()}", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        usuario = new Usuario(rd.GetInt32(0), rd.GetString(1), new List<Ambiente>());
                    }
                }
            }

            return usuario;
        }

        private Ambiente BuscaAmbientePorLog(int logId)
        {
            Ambiente ambiente = null;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 ID, NOME
                                                      FROM AMBIENTES AM ID = {logId.ToString()}", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        ambiente = new Ambiente(rd.GetInt32(0), rd.GetString(1), new Queue<Log>());
                    }
                }
            }

            return ambiente;
        }

        public List<Usuario> CarregarUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            using (SqlCommand query = new SqlCommand($@"SELECT ID, NOME FROM USUARIOS", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listaUsuarios.Add(new Usuario(rd.GetInt32(0), rd.GetString(1), new List<Ambiente>()));
                    }
                }
            }

            foreach (var r in listaUsuarios)
            {
                foreach (var x in CarregarAmbientesPorUsuario(r.Id))
                {
                    r.Ambientes.Add(x);
                }
            }

            return listaUsuarios;
        }

        public List<Ambiente> CarregarAmbientesPorUsuario(int usuarioId)
        {
            List<Ambiente> listaAmbientes = new List<Ambiente>();

            using (SqlCommand query = new SqlCommand($@"SELECT AM.ID, AM.NOME
                                                        FROM AMBIENTES_USUARIOS AU JOIN USUARIOS US
                                                        							 ON AU.ID = US.ID
                                                        						   JOIN AMBIENTES AM
                                                        						     ON AU.ID = AM.ID
                                                        WHERE US.ID = {usuarioId}
                                                        ", conexao))
            {
                using (SqlDataReader rd = query.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listaAmbientes.Add(new Ambiente(rd.GetInt32(0), rd.GetString(1), new Queue<Log>()));
                    }
                }
            }

            foreach (var r in listaAmbientes)
            {
                foreach (var x in BuscarLogsPorAmbiente(r.Id))
                {
                    r.Logs.Enqueue(x);
                }
            }

            return listaAmbientes;
        }

        private bool VerificaExistenciaLogs(int logId)
        {
            bool existe;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 ID FROM LOGS WHERE ID = {logId}
                                                        ", conexao))
            {
                existe = query.ExecuteScalar() == null ? false : true;
            }

            return existe;
        }

        public void GravarAmbientes(List<Ambiente> ambientes)
        {
            foreach (var r in ambientes)
            {
                if (!VerificaExistenciaAmbientes(r.Id))
                {
                    using (SqlCommand query = new SqlCommand($@"INSERT INTO AMBIENTES VALUES ({r.Id.ToString()},'{r.Nome}')", conexao))
                    {
                        query.ExecuteNonQuery();
                    }

                    foreach (var x in r.Logs)
                    {
                        GravarLogs(x, r.Id);
                    }
                }
            }
        }

        public void GravarLogs(Log log, int ambienteId)
        {
            if (!VerificaExistenciaLogs(log.Id))
            {
                using (SqlCommand query = new SqlCommand($@"INSERT INTO LOGS VALUES ('{log.DtAcesso.ToString("dd/MM/yyyy hh:mm:ss")}', '{(log.TipoAcesso ? 0 : 1).ToString()}',{log.Usuario.Id},{ambienteId})", conexao))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        private bool VerificaExistenciaAmbientes(int ambienteId)
        {
            bool existe;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 ID FROM AMBIENTES WHERE ID = {ambienteId}
                                                        ", conexao))
            {
                existe = query.ExecuteScalar() == null ? false : true;
            }

            return existe;
        }

        public void GravarUsuarios(List<Usuario> usuarios)
        {
            StringBuilder valores = new StringBuilder(String.Empty);

            using (SqlCommand query = new SqlCommand(@"DELETE FROM LOGS
                                                       DELETE FROM AMBIENTES
                                                       DELETE FROM USUARIOS", conexao))
            {
                query.ExecuteNonQuery();
            }

            if (usuarios.Count > 0)
            {
                foreach (var r in usuarios)
                {
                    if (!VerificaExistenciaUsuarios(r.Id))
                        valores.Append($",({r.Id.ToString()},'{r.Nome}')");
                }

                using (SqlCommand query = new SqlCommand($@"INSERT INTO USUARIOS VALUES {valores.ToString(1, valores.Length - 1)}", conexao))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        private bool VerificaExistenciaUsuarios(int usuarioId)
        {
            bool existe;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 ID FROM USUARIOS WHERE ID = {usuarioId}
                                                        ", conexao))
            {
                existe = query.ExecuteScalar() == null ? false : true;
            }

            return existe;
        }

        public void CarregarValoresIniciais(ref int a)
        {
            object aux;

            using (SqlCommand query = new SqlCommand($@"SELECT TOP 1 ID FROM LOGS ORDER BY ID DESC
                                                        ", conexao))
            {
                aux = query.ExecuteScalar();
                a = (int)(aux == null ? 1 : aux);
            }

        }
    }
}
