using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    public partial class MyMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "__efmigrationshistory",
                columns: table => new
                {
                    MigrationId = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductVersion = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.MigrationId);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    id_admin = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    hash_password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    salt = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_admin);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id_categories = table.Column<int>(type: "int", nullable: false),
                    title_category = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_categories);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "documents",
                columns: table => new
                {
                    id_documents = table.Column<int>(type: "int", nullable: false),
                    date_publication = table.Column<DateTime>(type: "date", nullable: false),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true),
                    link = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_documents);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id_events = table.Column<int>(type: "int", nullable: false),
                    title_event = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    text_event = table.Column<string>(type: "mediumtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_start = table.Column<DateTime>(type: "date", nullable: true),
                    date_end = table.Column<DateTime>(type: "date", nullable: true),
                    time_start = table.Column<TimeSpan>(type: "time", nullable: true),
                    time_end = table.Column<TimeSpan>(type: "time", nullable: true),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_events);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "news",
                columns: table => new
                {
                    id_news = table.Column<int>(type: "int", nullable: false),
                    title_news = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    text_news = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_publication = table.Column<DateTime>(type: "date", nullable: false),
                    date_delete = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_news);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "posters",
                columns: table => new
                {
                    id_poster = table.Column<int>(type: "int", nullable: false),
                    title_poster = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    text_poster = table.Column<string>(type: "mediumtext", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_start = table.Column<DateTime>(type: "date", nullable: true),
                    date_end = table.Column<DateTime>(type: "date", nullable: true),
                    date_publication = table.Column<DateTime>(type: "date", nullable: true),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_poster);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id_report = table.Column<int>(type: "int", nullable: false),
                    date_publication = table.Column<DateTime>(type: "date", nullable: false),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true),
                    link = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_report);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "categories_in_documents",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: true),
                    id_document = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "id_category_document",
                        column: x => x.id_category,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "id_document_docement",
                        column: x => x.id_document,
                        principalTable: "documents",
                        principalColumn: "id_documents",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "category_in_events",
                columns: table => new
                {
                    id_category_in_events = table.Column<int>(type: "int", nullable: true),
                    id_event = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "id_category_in_events",
                        column: x => x.id_category_in_events,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "id_event",
                        column: x => x.id_event,
                        principalTable: "events",
                        principalColumn: "id_events",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "category_in_news",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: false),
                    id_news = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "id_category",
                        column: x => x.id_category,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "id_news_category",
                        column: x => x.id_news,
                        principalTable: "news",
                        principalColumn: "id_news",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    idcomments = table.Column<int>(type: "int", nullable: false),
                    name_commentator = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    text_comment = table.Column<string>(type: "mediumtext", nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_news = table.Column<int>(type: "int", nullable: false),
                    date_publication = table.Column<DateTime>(type: "date", nullable: true),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idcomments);
                    table.ForeignKey(
                        name: "id_news",
                        column: x => x.id_news,
                        principalTable: "news",
                        principalColumn: "id_news",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "photo_in_news",
                columns: table => new
                {
                    id_photo = table.Column<int>(type: "int", nullable: false),
                    id_news = table.Column<int>(type: "int", nullable: true),
                    link = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    date_delete = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_photo);
                    table.ForeignKey(
                        name: "id_news_photo",
                        column: x => x.id_news,
                        principalTable: "news",
                        principalColumn: "id_news",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "category_in_posters",
                columns: table => new
                {
                    idCategoryInPoster = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_category = table.Column<int>(type: "int", nullable: true),
                    id_poster = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.idCategoryInPoster);
                    table.ForeignKey(
                        name: "id_category_posters",
                        column: x => x.id_category,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "id_poster",
                        column: x => x.id_poster,
                        principalTable: "posters",
                        principalColumn: "id_poster",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "category_in_reports",
                columns: table => new
                {
                    id_category = table.Column<int>(type: "int", nullable: true),
                    id_report = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "id_category_reports",
                        column: x => x.id_category,
                        principalTable: "categories",
                        principalColumn: "id_categories",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "id_report",
                        column: x => x.id_report,
                        principalTable: "reports",
                        principalColumn: "id_report",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "id_category_document_idx",
                table: "categories_in_documents",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "id_document_docement_idx",
                table: "categories_in_documents",
                column: "id_document");

            migrationBuilder.CreateIndex(
                name: "id_category_in_events_idx",
                table: "category_in_events",
                column: "id_category_in_events");

            migrationBuilder.CreateIndex(
                name: "id_event_idx",
                table: "category_in_events",
                column: "id_event");

            migrationBuilder.CreateIndex(
                name: "id_category_idx",
                table: "category_in_news",
                column: "id_news");

            migrationBuilder.CreateIndex(
                name: "id_category_idx1",
                table: "category_in_news",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "id_category_posters_idx",
                table: "category_in_posters",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "id_poster_idx",
                table: "category_in_posters",
                column: "id_poster");

            migrationBuilder.CreateIndex(
                name: "id_category_reports_idx",
                table: "category_in_reports",
                column: "id_category");

            migrationBuilder.CreateIndex(
                name: "id_report_idx",
                table: "category_in_reports",
                column: "id_report");

            migrationBuilder.CreateIndex(
                name: "_idx",
                table: "comments",
                column: "id_news");

            migrationBuilder.CreateIndex(
                name: "id_news_photo_idx",
                table: "photo_in_news",
                column: "id_news");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "__efmigrationshistory");

            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "categories_in_documents");

            migrationBuilder.DropTable(
                name: "category_in_events");

            migrationBuilder.DropTable(
                name: "category_in_news");

            migrationBuilder.DropTable(
                name: "category_in_posters");

            migrationBuilder.DropTable(
                name: "category_in_reports");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "photo_in_news");

            migrationBuilder.DropTable(
                name: "documents");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "posters");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "news");
        }
    }
}
