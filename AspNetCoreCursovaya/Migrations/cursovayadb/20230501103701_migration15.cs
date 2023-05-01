using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class migration15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdCategoryInDocument",
                table: "categories_in_documents",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PRIMARY",
                table: "categories_in_documents",
                column: "IdCategoryInDocument");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PRIMARY",
                table: "categories_in_documents");

            migrationBuilder.DropColumn(
                name: "IdCategoryInDocument",
                table: "categories_in_documents");
        }
    }
}
