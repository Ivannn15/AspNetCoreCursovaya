using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class MyMigrt123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPartner",
                table: "CategoryInPartners",
                newName: "id_partner");

            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "CategoryInPartners",
                newName: "id_category");

            migrationBuilder.RenameColumn(
                name: "idCategoryInPartners",
                table: "CategoryInPartners",
                newName: "idCategory_in_partners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id_partner",
                table: "CategoryInPartners",
                newName: "IdPartner");

            migrationBuilder.RenameColumn(
                name: "id_category",
                table: "CategoryInPartners",
                newName: "IdCategory");

            migrationBuilder.RenameColumn(
                name: "idCategory_in_partners",
                table: "CategoryInPartners",
                newName: "idCategoryInPartners");
        }
    }
}
