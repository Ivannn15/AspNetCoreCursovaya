using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class Migration28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    IdPartner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TitlePartner = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TextPartner = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateDelete = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    linkPhoto = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.IdPartner);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "CategoryInPartners",
                columns: table => new
                {
                    idCategoryInPartners = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IdCategory = table.Column<int>(type: "int", nullable: true),
                    IdPartner = table.Column<int>(type: "int", nullable: true),
                    IdCategoryNavigationIdCategories = table.Column<int>(type: "int", nullable: true),
                    IdPartnerNavigationIdPartner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryInPartners", x => x.idCategoryInPartners);
                    table.ForeignKey(
                        name: "FK_CategoryInPartners_categories_IdCategoryNavigationIdCategori~",
                        column: x => x.IdCategoryNavigationIdCategories,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryInPartners_Partners_IdPartnerNavigationIdPartner",
                        column: x => x.IdPartnerNavigationIdPartner,
                        principalTable: "Partners",
                        principalColumn: "IdPartner",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryInPartners_IdCategoryNavigationIdCategories",
                table: "CategoryInPartners",
                column: "IdCategoryNavigationIdCategories");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryInPartners_IdPartnerNavigationIdPartner",
                table: "CategoryInPartners",
                column: "IdPartnerNavigationIdPartner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryInPartners");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
