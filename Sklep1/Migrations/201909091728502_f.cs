namespace Sklep1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(),
                        OrderStatus = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        ImageUrl = c.String(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Products", new[] { "Order_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
        }
    }
}
