namespace Aruba.Eis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version100r1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ActivityResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        RoleId = c.String(maxLength: 128),
                        MinOccurs = c.Int(nullable: false),
                        MaxOccurs = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.ActivityId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 255),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Hometown = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ScheduleResources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        RoleId = c.String(maxLength: 128),
                        MinOccurs = c.Int(nullable: false),
                        MaxOccors = c.Int(nullable: false),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId)
                .Index(t => t.ScheduleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50),
                        Name = c.String(nullable: false, maxLength: 255),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Assignements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ScheduleId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        RoleId = c.String(maxLength: 128),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(maxLength: 50),
                        DateModified = c.DateTime(),
                        UserModified = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.Schedules", t => t.ScheduleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ScheduleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleResources", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Assignements", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Assignements", "ScheduleId", "dbo.Schedules");
            DropForeignKey("dbo.Assignements", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ScheduleResources", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TeamResources", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamResources", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.ActivityResources", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ActivityResources", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Assignements", new[] { "RoleId" });
            DropIndex("dbo.Assignements", new[] { "UserId" });
            DropIndex("dbo.Assignements", new[] { "ScheduleId" });
            DropIndex("dbo.ScheduleResources", new[] { "RoleId" });
            DropIndex("dbo.ScheduleResources", new[] { "ScheduleId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TeamResources", new[] { "UserId" });
            DropIndex("dbo.TeamResources", new[] { "TeamId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ActivityResources", new[] { "RoleId" });
            DropIndex("dbo.ActivityResources", new[] { "ActivityId" });
            DropTable("dbo.Assignements");
            DropTable("dbo.Schedules");
            DropTable("dbo.ScheduleResources");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TeamResources");
            DropTable("dbo.Teams");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ActivityResources");
            DropTable("dbo.Activities");
        }
    }
}
