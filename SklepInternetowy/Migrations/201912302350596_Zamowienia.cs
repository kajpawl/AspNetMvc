namespace SklepInternetowy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zamowienia : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Zamowienies", name: "ApplicationUser_Id", newName: "UserId");
            RenameIndex(table: "dbo.Zamowienies", name: "IX_ApplicationUser_Id", newName: "IX_UserId");
            AddColumn("dbo.Zamowienies", "Adres", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy", c => c.String());
            AlterColumn("dbo.Zamowienies", "Telefon", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Zamowienies", "Email", c => c.String(nullable: false));
            DropColumn("dbo.Zamowienies", "Ulica");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Zamowienies", "Ulica", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Zamowienies", "Email", c => c.String());
            AlterColumn("dbo.Zamowienies", "Telefon", c => c.String());
            DropColumn("dbo.AspNetUsers", "DaneUzytkownika_KodPocztowy");
            DropColumn("dbo.Zamowienies", "Adres");
            RenameIndex(table: "dbo.Zamowienies", name: "IX_UserId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Zamowienies", name: "UserId", newName: "ApplicationUser_Id");
        }
    }
}
