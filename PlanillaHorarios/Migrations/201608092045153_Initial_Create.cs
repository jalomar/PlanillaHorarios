namespace PlanillaHorarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Feriado",
                c => new
                {
                    FeriadoID = c.Int(nullable: false, identity: true),
                    Fecha = c.DateTime(nullable: false),
                    Descripcion = c.String(),
                })
                .PrimaryKey(t => t.FeriadoID);

            CreateTable(
                "dbo.Persona",
                c => new
                {
                    PersonaID = c.Int(nullable: false, identity: true),
                    Nombre = c.String(),
                    Apellido = c.String(),
                })
                .PrimaryKey(t => t.PersonaID);

            CreateTable(
               "dbo.Planilla",
               c => new
               {
                   PlanillaId = c.Int(nullable: false, identity: true),
                   PersonaId = c.Int(nullable: false),
                   Fecha = c.DateTime(nullable: false),
                   Dia = c.Int(nullable: false),
                   MHoraEntrada = c.Int(nullable: true),
                   MHoraSalida = c.Int(nullable: true),
                   THoraEntrada = c.Int(nullable: true),
                   THoraSalida = c.Int(nullable: true),
                   Observaciones = c.String(),
               })
               .PrimaryKey(t => t.PlanillaId)
               .ForeignKey("dbo.Persona", t => t.PersonaId, cascadeDelete: true);

        }

public override void Down()
        {
            
            DropIndex("dbo.Planilla", new[] { "PlanillaID" });
            DropForeignKey("dbo.Planilla", "PersonaID", "dbo.Persona");
            DropTable("dbo.Planilla");
            DropIndex("dbo.Persona", new[] { "PersonaID" });
            DropTable("dbo.Persona");
            DropIndex("dbo.Feriado", new[] { "FeriadoID" });
            DropTable("dbo.Feriado");

        }
    }
}
