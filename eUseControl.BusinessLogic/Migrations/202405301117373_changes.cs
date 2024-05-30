namespace eUseControl.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Playlists", "UserSignUp_UserId", "dbo.UserSignUps");
            DropIndex("dbo.Playlists", new[] { "UserSignUp_UserId" });
            DropColumn("dbo.Playlists", "UserSignUpId");
            DropColumn("dbo.Playlists", "UserSignUp_UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "UserSignUp_UserId", c => c.Int());
            AddColumn("dbo.Playlists", "UserSignUpId", c => c.Int(nullable: false));
            CreateIndex("dbo.Playlists", "UserSignUp_UserId");
            AddForeignKey("dbo.Playlists", "UserSignUp_UserId", "dbo.UserSignUps", "UserId");
        }
    }
}
