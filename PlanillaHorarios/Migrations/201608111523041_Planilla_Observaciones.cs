namespace PlanillaHorarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Planilla_Observaciones : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Planilla", "Observaciones", c => c.String());
            //DropColumn("dbo.Planilla", "Observacion");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Planilla", "Observacion", c => c.String());
            //DropColumn("dbo.Planilla", "Observaciones");
        }
    }
}
