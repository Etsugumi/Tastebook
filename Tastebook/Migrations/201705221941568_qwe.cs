namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qwe : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "AuthorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "AuthorId", c => c.String(nullable: false));
        }
    }
}
