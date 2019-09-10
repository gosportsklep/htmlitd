namespace Sklep1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderids : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_Id" });
            AddColumn("dbo.Orders", "productsIds", c => c.String());
            DropColumn("dbo.Products", "Order_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Order_Id", c => c.Int());
            DropColumn("dbo.Orders", "productsIds");
            CreateIndex("dbo.Products", "Order_Id");
            AddForeignKey("dbo.Products", "Order_Id", "dbo.Orders", "Id");
        }
    }
}
