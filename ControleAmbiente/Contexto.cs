using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleAmbiente
{
    class Contexto : DbContext
    {
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Usuario> Usuarios{ get; set; }
        public virtual DbSet<Ambiente> Ambientes{ get; set; }
    }
}
