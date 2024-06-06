namespace eUseControl.BusinessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DataTime : DbMigration
    {
          public override void Up()
          {
               AlterColumn("dbo.Musics", "Created", c => c.DateTime(nullable: true));
               AlterColumn("dbo.Musics", "Updated", c => c.DateTime(nullable: true));
          }

          public override void Down()
          {
               AlterColumn("dbo.Musics", "Created", c => c.DateTime(nullable: false));
               AlterColumn("dbo.Musics", "Updated", c => c.DateTime(nullable: false));
          }

     }
}
