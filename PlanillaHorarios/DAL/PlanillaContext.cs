using PlanillaHorarios.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanillaHorarios.DAL
{
    class PlanillaContext : DbContext
    {
        public PlanillaContext()
        {
           // Database.SetInitializer<PlanillaContext>(new CreateDatabaseIfNotExists<PlanillaContext>());
            //DropCreateDatabaseAlways
            //DropCreateDatabaseIfModelChanges
        }

        //public PlanillaContext() : base("DefaultConnection") { }
        // Si el nombre dl Context es el mismo nombre dl stringConnection no necesita pasarse como parámetro

        public DbSet<Persona> Persona { get; set; }
        public DbSet<Feriado> Feriado { get; set; }
        public DbSet<Planilla> Planilla { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
