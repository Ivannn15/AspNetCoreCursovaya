using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class MyMigrt63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_categories_id_category_partnerIdCategor~",
                table: "category_in_partners");

            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_partners_id_partner_partneridPartners",
                table: "category_in_partners");

            migrationBuilder.DropIndex(
                name: "IX_category_in_partners_id_category_partnerIdCategories",
                table: "category_in_partners");

            migrationBuilder.DropIndex(
                name: "IX_category_in_partners_id_partner_partneridPartners",
                table: "category_in_partners");

            migrationBuilder.DropColumn(
                name: "id_category_partnerIdCategories",
                table: "category_in_partners");

            migrationBuilder.DropColumn(
                name: "id_partner_partneridPartners",
                table: "category_in_partners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_category_partnerIdCategories",
                table: "category_in_partners",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_partner_partneridPartners",
                table: "category_in_partners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_in_partners_id_category_partnerIdCategories",
                table: "category_in_partners",
                column: "id_category_partnerIdCategories");

            migrationBuilder.CreateIndex(
                name: "IX_category_in_partners_id_partner_partneridPartners",
                table: "category_in_partners",
                column: "id_partner_partneridPartners");

            migrationBuilder.AddForeignKey(
                name: "FK_category_in_partners_categories_id_category_partnerIdCategor~",
                table: "category_in_partners",
                column: "id_category_partnerIdCategories",
                principalTable: "categories",
                principalColumn: "id_categories",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_category_in_partners_partners_id_partner_partneridPartners",
                table: "category_in_partners",
                column: "id_partner_partneridPartners",
                principalTable: "partners",
                principalColumn: "idPartners",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
