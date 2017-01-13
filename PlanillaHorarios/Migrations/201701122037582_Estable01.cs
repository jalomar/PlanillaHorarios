namespace PlanillaHorarios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Estable01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Persona", "Cuil", c => c.String(nullable: false));
            AddColumn("dbo.Persona", "Mail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Persona", "Mail");
            DropColumn("dbo.Persona", "Cuil");
        }
    }
}
