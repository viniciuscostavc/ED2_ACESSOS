using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Ambiente
    {
        private int id;
        private string nome;
        private Queue<Log> logs;
        private int maxLogs = 100;

        public int Id { get => id; set { this.id = value; } }
        public string Nome { get => nome; set { this.nome = value; } }
        public Queue<Log> Logs { get => logs; }

        public Ambiente(int id, string nome, Queue<Log> logs)
        {
            this.id = id;
            this.nome = nome;
            this.logs = logs;
        }

        public void registrarLog(Log log)
        {
            if(this.logs.Count() >= 100)
            {
                this.logs.Dequeue();
                this.logs.Enqueue(log);
                return;
            }

            this.logs.Enqueue(log);
        }

        public override bool Equals(object obj)
        {
            var item = obj as Ambiente;

            if (this.id.Equals(item.Id))
                return true;

            return false;
        }

        public string toString()
        {
            return String.Concat(" ID: ", this.id.ToString()," | ", " Nome do ambiente: ", this.nome);
        }
    }
}
