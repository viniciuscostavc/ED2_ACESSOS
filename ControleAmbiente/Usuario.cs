using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Usuario
    {
        private int id;
        private string nome;
        private List<Ambiente> ambientes;

        public int Id { get => id; set { this.id = value; } }
        public string Nome { get => nome; set { this.nome = value; } }
        public List<Ambiente> Ambientes { get => ambientes; set { this.ambientes = value; } }

        public Usuario(int id, string nome, List<Ambiente> ambientes)
        {
            this.id = id;
            this.nome = nome;
            this.ambientes = ambientes;
        }

        public bool concederPermissao(Ambiente ambiente)
        {
            if (verificaAmbienteJaCadastrado(ambiente))
                return false;

            this.ambientes.Add(ambiente);

            return true;
        }

        public bool revogarPermissao(Ambiente ambiente)
        {
            if (!verificaAmbienteJaCadastrado(ambiente))
                return false;

            this.ambientes.Remove(ambiente);

            return true;
        }

        public bool possuiPermissao(Ambiente ambiente)
        {
            return (this.ambientes.Where(x => x.Equals(ambiente)).ToList().Count > 0 ? true : false);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Usuario;

            if (this.id.Equals(item.Id))
                return true;

            return false;
        }

        public bool possuiAmbientes()
        {
            return (this.ambientes.Count > 0 ? true : false);
        }

        private bool verificaAmbienteJaCadastrado(Ambiente ambiente)
        {
            return (ambientes.Where(x => x.Equals(ambiente)).ToList().Count > 0 ? true : false);
        }

        public string toString()
        {
            return String.Concat(" ID: ", this.id.ToString(), " | ", " Nome do usuario: ", this.nome);
        }
    }
}
