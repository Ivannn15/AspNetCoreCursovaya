using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class MyMigrt83 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_categories_IdCategoryNavigationIdCatego~",
                table: "category_in_partners");

            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners");

            migrationBuilder.RenameColumn(
                name: "IdDocumentNavigationidPartners",
                table: "category_in_partners",
                newName: "id_partner_partneridPartners");

            migrationBuilder.RenameColumn(
                name: "IdCategoryNavigationIdCategories",
                table: "category_in_partners",
                newName: "id_category_partnerIdCategories");

            migrationBuilder.RenameIndex(
                name: "IX_category_in_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners",
                newName: "IX_category_in_partners_id_partner_partneridPartners");

            migrationBuilder.RenameIndex(
                name: "IX_category_in_partners_IdCategoryNavigationIdCategories",
                table: "category_in_partners",
                newName: "IX_category_in_partners_id_category_partnerIdCategories");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_categories_id_category_partnerIdCategor~",
                table: "category_in_partners");

            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_partners_id_partner_partneridPartners",
                table: "category_in_partners");

            migrationBuilder.RenameColumn(
                name: "id_partner_partneridPartners",
                table: "category_in_partners",
                newName: "IdDocumentNavigationidPartners");

            migrationBuilder.RenameColumn(
                name: "id_category_partnerIdCategories",
                table: "category_in_partners",
                newName: "IdCategoryNavigationIdCategories");

            migrationBuilder.RenameIndex(
                name: "IX_category_in_partners_id_partner_partneridPartners",
                table: "category_in_partners",
                newName: "IX_category_in_partners_IdDocumentNavigationidPartners");

            migrationBuilder.RenameIndex(
                name: "IX_category_in_partners_id_category_partnerIdCategories",
                table: "category_in_partners",
                newName: "IX_category_in_partners_IdCategoryNavigationIdCategories");

            migrationBuilder.AddForeignKey(
                name: "FK_category_in_partners_categories_IdCategoryNavigationIdCatego~",
                table: "category_in_partners",
                column: "IdCategoryNavigationIdCategories",
                principalTable: "categories",
                principalColumn: "id_categories",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_category_in_partners_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners",
                column: "IdDocumentNavigationidPartners",
                principalTable: "partners",
                principalColumn: "idPartners",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
