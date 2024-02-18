using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class MyMigrt333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryInPartners",
                table: "CategoryInPartners");

            migrationBuilder.RenameTable(
                name: "CategoryInPartners",
                newName: "category_in_partners");

            migrationBuilder.AddColumn<int>(
                name: "IdCategoryNavigationIdCategories",
                table: "category_in_partners",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdDocumentNavigationidPartners",
                table: "category_in_partners",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_category_in_partners",
                table: "category_in_partners",
                column: "idCategory_in_partners");

            migrationBuilder.CreateIndex(
                name: "IX_category_in_partners_IdCategoryNavigationIdCategories",
                table: "category_in_partners",
                column: "IdCategoryNavigationIdCategories");

            migrationBuilder.CreateIndex(
                name: "IX_category_in_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners",
                column: "IdDocumentNavigationidPartners");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_categories_IdCategoryNavigationIdCatego~",
                table: "category_in_partners");

            migrationBuilder.DropForeignKey(
                name: "FK_category_in_partners_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category_in_partners",
                table: "category_in_partners");

            migrationBuilder.DropIndex(
                name: "IX_category_in_partners_IdCategoryNavigationIdCategories",
                table: "category_in_partners");

            migrationBuilder.DropIndex(
                name: "IX_category_in_partners_IdDocumentNavigationidPartners",
                table: "category_in_partners");

            migrationBuilder.DropColumn(
                name: "IdCategoryNavigationIdCategories",
                table: "category_in_partners");

            migrationBuilder.DropColumn(
                name: "IdDocumentNavigationidPartners",
                table: "category_in_partners");

            migrationBuilder.RenameTable(
                name: "category_in_partners",
                newName: "CategoryInPartners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryInPartners",
                table: "CategoryInPartners",
                column: "idCategory_in_partners");
        }
    }
}
