namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilesChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "FullName", c => c.String());
            AddColumn("dbo.Images", "Extension", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "Extension");
            DropColumn("dbo.Images", "FullName");
        }
    }
}
