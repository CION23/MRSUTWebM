namespace eUseControl.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "LastPlayedTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Playlists", "UserSignUpId", c => c.Int(nullable: false));
            AddColumn("dbo.Playlists", "UserSignUp_UserId", c => c.Int());
            CreateIndex("dbo.Playlists", "UserSignUp_UserId");
            AddForeignKey("dbo.Playlists", "UserSignUp_UserId", "dbo.UserSignUps", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playlists", "UserSignUp_UserId", "dbo.UserSignUps");
            DropIndex("dbo.Playlists", new[] { "UserSignUp_UserId" });
            DropColumn("dbo.Playlists", "UserSignUp_UserId");
            DropColumn("dbo.Playlists", "UserSignUpId");
            DropColumn("dbo.Musics", "LastPlayedTime");
        }
    }
}
