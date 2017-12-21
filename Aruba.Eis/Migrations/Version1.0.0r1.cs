namespace Aruba.Eis.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Version100r1 : DbMigration
    {
        public override void Up()
        {
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
            
            DropColumn("dbo.AspNetUsers", "ArubaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ArubaId", c => c.String());
            DropForeignKey("dbo.TeamResources", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamResources", "TeamId", "dbo.Teams");
            DropIndex("dbo.TeamResources", new[] { "UserId" });
            DropIndex("dbo.TeamResources", new[] { "TeamId" });
            DropTable("dbo.TeamResources");
        }
    }
}
