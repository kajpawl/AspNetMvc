namespace SklepInternetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class Elmah : DbMigration
    {
        public override void Up()
        {
            SqlFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Migrations", "ELMAH-1.2-db-SQLServer.sql"));
        }
        
        public override void Down()
        {
        }
    }
}
