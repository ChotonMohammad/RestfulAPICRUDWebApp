namespace EmployeeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSettings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        MenuItemId = c.Int(nullable: false, identity: true),
                        MenuItemName = c.String(nullable: false, maxLength: 50),
                        MenuItemShortName = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.MenuItemId)
                .Index(t => t.MenuItemShortName, unique: true, name: "UK_MenuItemShortName");
            
            CreateTable(
                "dbo.MenuSettings",
                c => new
                    {
                        MenuSettingId = c.Int(nullable: false, identity: true),
                        MenuItemId = c.Int(nullable: false),
                        ViewStatus = c.Int(nullable: false),
                        CreateStatus = c.Int(nullable: false),
                        UpdateStatus = c.Int(nullable: false),
                        DeleteStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuSettingId)
                .ForeignKey("dbo.MenuItems", t => t.MenuItemId)
                .Index(t => t.MenuItemId, unique: true, name: "UK_MenuSetting");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuSettings", "MenuItemId", "dbo.MenuItems");
            DropIndex("dbo.MenuSettings", "UK_MenuSetting");
            DropIndex("dbo.MenuItems", "UK_MenuItemShortName");
            DropTable("dbo.MenuSettings");
            DropTable("dbo.MenuItems");
        }
    }
}
