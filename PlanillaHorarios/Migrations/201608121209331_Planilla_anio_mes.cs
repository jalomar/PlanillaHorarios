namespace PlanillaHorarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Planilla_anio_mes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planilla", "Anio", c => c.Int());
            AddColumn("dbo.Planilla", "Mes", c => c.Int());
            DropColumn("dbo.Planilla", "Fecha");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planilla", "Fecha", c => c.DateTime(nullable: false));
            DropColumn("dbo.Planilla", "Mes");
            DropColumn("dbo.Planilla", "Anio");
        }
    }
}
