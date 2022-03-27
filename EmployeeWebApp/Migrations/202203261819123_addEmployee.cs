namespace EmployeeWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmployee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(nullable: false, maxLength: 50),
                        IsDeleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .Index(t => t.CityName, unique: true, name: "UK_City");
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        EmployeeName = c.String(nullable: false, maxLength: 55),
                        Gender = c.Int(nullable: false),
                        DepartmentId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        IsDeleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .ForeignKey("dbo.Departments", t => t.DepartmentId)
                .Index(t => t.DepartmentId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(nullable: false, maxLength: 55),
                        IsDeleted = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .Index(t => t.DepartmentName, unique: true, name: "UK_Department");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Employees", "CityId", "dbo.Cities");
            DropIndex("dbo.Departments", "UK_Department");
            DropIndex("dbo.Employees", new[] { "CityId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Cities", "UK_City");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Cities");
        }
    }
}
