using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Log
    {
        private int id;
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipoAcesso;

        public int Id { get => this.id; }
        public Usuario Usuario { get => this.usuario; set { this.usuario = value; } }
        public DateTime DtAcesso { get => this.dtAcesso; set { this.dtAcesso = value; } }
        public bool TipoAcesso { get => this.tipoAcesso; set { this.tipoAcesso = value; } }

        public Log(int id, DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
        {
            this.id = id;
            this.dtAcesso = dtAcesso;
            this.usuario = usuario;
            this.tipoAcesso = tipoAcesso;
        }

        public string toString()
        {
            return String.Concat(" Data de acesso: ", this.dtAcesso.ToString("dd/MM/yyyy"), " |", this.usuario.toString(), " | Acesso: ", (this.tipoAcesso ? "Autorizado" : "Negado")).ToString();
        }
    }
}
