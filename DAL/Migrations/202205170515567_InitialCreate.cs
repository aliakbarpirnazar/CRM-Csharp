﻿namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Info = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        Category_id = c.Int(),
                        Customers_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ActivityCategories", t => t.Category_id)
                .ForeignKey("dbo.Customers", t => t.Customers_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Category_id)
                .Index(t => t.Customers_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.ActivityCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                        Phone = c.String(),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        CheckoutDate = c.DateTime(nullable: false),
                        IsCheckout = c.Boolean(nullable: false),
                        Customers_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.Customers_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Customers_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Pic = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                        MyProperty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReminderInfo = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        RemindDate = c.DateTime(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.ProductInvoices",
                c => new
                    {
                        Product_id = c.Int(nullable: false),
                        Invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_id, t.Invoice_id })
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.Invoice_id, cascadeDelete: true)
                .Index(t => t.Product_id)
                .Index(t => t.Invoice_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reminders", "User_id", "dbo.Users");
            DropForeignKey("dbo.Invoices", "User_id", "dbo.Users");
            DropForeignKey("dbo.Activities", "User_id", "dbo.Users");
            DropForeignKey("dbo.ProductInvoices", "Invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.ProductInvoices", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Invoices", "Customers_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "Customers_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "Category_id", "dbo.ActivityCategories");
            DropIndex("dbo.ProductInvoices", new[] { "Invoice_id" });
            DropIndex("dbo.ProductInvoices", new[] { "Product_id" });
            DropIndex("dbo.Reminders", new[] { "User_id" });
            DropIndex("dbo.Invoices", new[] { "User_id" });
            DropIndex("dbo.Invoices", new[] { "Customers_id" });
            DropIndex("dbo.Activities", new[] { "User_id" });
            DropIndex("dbo.Activities", new[] { "Customers_id" });
            DropIndex("dbo.Activities", new[] { "Category_id" });
            DropTable("dbo.ProductInvoices");
            DropTable("dbo.Reminders");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.ActivityCategories");
            DropTable("dbo.Activities");
        }
    }
}
