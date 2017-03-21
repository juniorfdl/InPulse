using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using Models;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Infra.Base
{
    public class Context : DbContext
    {
        public Context() : base("name=crm_sgr")
        {
            Database.SetInitializer<Context>(null);
            Database.Initialize(false);
            //var ip = GetUserIp();
                       

        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            base.Configuration.LazyLoadingEnabled = false;
            
            modelBuilder.Conventions.AddBefore<ForeignKeyIndexConvention>(new ForeignKeyNamingConvention());

            //modelBuilder.Entity<CAD_COND_PAGAMENTO>();
            //modelBuilder.Entity<CAD_COND_PAGAMENTO_DIAS>();

            //modelBuilder.Entity<FAT_NF_SERVICO>();
            //modelBuilder.Entity<FAT_NF_SERVICO_ITEM>();
        }

        public virtual DbSet<operadores> operadores { get; set; }

        public virtual DbSet<operadores_status> operadores_status { get; set; }
        public virtual DbSet<operadores_foto> operadores_foto { get; set; }

        public virtual DbSet<v_operadores_status> v_operadores_status { get; set; }
    }
}
