namespace eUseControl.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserLoginDataId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Musics", "UserSignUpId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Musics", "UserSignUpId");
        }
    }
}
