using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Cadastro
    {
        private List<Usuario> usuarios;
        private List<Ambiente> ambientes;
        private Repositorio repositorio;
        private int ultimoIdLog;

        public int UltimoIdLog { get => this.ultimoIdLog; set { this.ultimoIdLog = value; } }
        
        public Cadastro()
        {
            this.usuarios = new List<Usuario>();
            this.ambientes = new List<Ambiente>();
            this.repositorio = new Repositorio();

            download();
        }

        public void adicionarUsuario(Usuario usuario)
        {
            if (pesquisarUsuario(usuario) != null)
                return;

            if (String.IsNullOrEmpty(usuario.Nome) || String.IsNullOrEmpty(usuario.Id.ToString()) || usuario.Id.ToString().Equals("0"))
                return;

            this.usuarios.Add(usuario);
        }

        public bool removerUsuario(Usuario usuario)
        {
            Usuario usuarioPesquisado = pesquisarUsuario(usuario);

            if (usuarioPesquisado == null)
                return false;

            if (usuarioPesquisado.possuiAmbientes())
                return false;

            usuarios.Remove(usuario);

            return true;
        }

        public Usuario pesquisarUsuario(Usuario usuario)
        {
            return this.usuarios.Where(x => x.Equals(usuario)).FirstOrDefault();
        }

        public List<Usuario> pesquisarUsuarios(Usuario usuario)
        {
            List<Usuario> usuariosPesquisados = this.usuarios;

            if (usuario.Id != 0)
            {
                usuariosPesquisados = usuariosPesquisados.Where(x => x.Equals(usuario)).ToList();
            }

            if (!String.IsNullOrEmpty(usuario.Nome))
            {
                usuariosPesquisados = usuariosPesquisados.Where(x => x.Nome.Equals(usuario.Nome)).ToList();
            }
            return usuariosPesquisados;
        }

        public void adicionarAmbiente(Ambiente ambiente)
        {
            if (pesquisarAmbiente(ambiente) != null)
                return;

            if (String.IsNullOrEmpty(ambiente.Nome) || String.IsNullOrEmpty(ambiente.Id.ToString()) || ambiente.Id.ToString().Equals("0"))
                return;

                this.ambientes.Add(ambiente);
        }

        public bool removerAmbiente(Ambiente ambiente)
        {
            if (pesquisarAmbiente(ambiente) == null)
                return false;

            this.ambientes.Remove(ambiente);

            return true;
        }

        public Ambiente pesquisarAmbiente(Ambiente ambiente)
        {
            return this.ambientes.Where(x => x.Equals(ambiente)).FirstOrDefault();
        }

        public List<Ambiente> pesquisarAmbientes(Ambiente ambiente)
        {
            List<Ambiente> ambientesPesquisados = this.ambientes;

            if (ambiente.Id != 0)
            {
                ambientesPesquisados = ambientesPesquisados.Where(x => x.Equals(ambiente)).ToList();
            }

            if (!String.IsNullOrEmpty(ambiente.Nome))
            {
                ambientesPesquisados = ambientesPesquisados.Where(x => x.Nome.Equals(ambiente.Nome)).ToList();
            }
            return ambientesPesquisados;
        }

        public void upload()
        {
            repositorio.GravarUsuarios(usuarios);
            repositorio.GravarAmbientes(ambientes);
        }

        public void download()
        {
            usuarios = repositorio.CarregarUsuarios();
            ambientes = repositorio.CarregarAmbientes();
            repositorio.CarregarValoresIniciais(ref this.ultimoIdLog);
        }
    }
}
