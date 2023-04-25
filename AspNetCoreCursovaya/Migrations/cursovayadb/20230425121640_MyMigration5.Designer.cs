﻿// <auto-generated />
using System;
using AspNetCoreCursovaya.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNetCoreCursovaya.Migrations.cursovayadb
{
    [DbContext(typeof(cursovayadbContext))]
    [Migration("20230425121640_MyMigration5")]
    partial class MyMigration5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Admin", b =>
                {
                    b.Property<int>("IdAdmin")
                        .HasColumnType("int")
                        .HasColumnName("id_admin");

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("hash_password");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("salt");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("username");

                    b.HasKey("IdAdmin")
                        .HasName("PRIMARY");

                    b.ToTable("admin");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoriesInDocument", b =>
                {
                    b.Property<int?>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("id_category");

                    b.Property<int?>("IdDocument")
                        .HasColumnType("int")
                        .HasColumnName("id_document");

                    b.HasIndex(new[] { "IdCategory" }, "id_category_document_idx");

                    b.HasIndex(new[] { "IdDocument" }, "id_document_docement_idx");

                    b.ToTable("categories_in_documents");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Category", b =>
                {
                    b.Property<int>("IdCategories")
                        .HasColumnType("int")
                        .HasColumnName("id_categories");

                    b.Property<string>("TitleCategory")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("title_category");

                    b.HasKey("IdCategories")
                        .HasName("PRIMARY");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInEvent", b =>
                {
                    b.Property<int?>("id_category_event_record")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("IdCategoryInEvents")
                        .HasColumnType("int")
                        .HasColumnName("id_category_in_events");

                    b.Property<int?>("IdEvent")
                        .HasColumnType("int")
                        .HasColumnName("id_event");

                    b.HasKey("id_category_event_record")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCategoryInEvents" }, "id_category_in_events_idx");

                    b.HasIndex(new[] { "IdEvent" }, "id_event_idx");

                    b.ToTable("category_in_events");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInNews", b =>
                {
                    b.Property<int>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("id_category");

                    b.Property<int>("IdNews")
                        .HasColumnType("int")
                        .HasColumnName("id_news");

                    b.HasIndex(new[] { "IdNews" }, "id_category_idx");

                    b.HasIndex(new[] { "IdCategory" }, "id_category_idx1");

                    b.ToTable("category_in_news");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInPoster", b =>
                {
                    b.Property<int?>("idCategoryInPoster")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("id_category");

                    b.Property<int?>("IdPoster")
                        .HasColumnType("int")
                        .HasColumnName("id_poster");

                    b.HasKey("idCategoryInPoster")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdCategory" }, "id_category_posters_idx");

                    b.HasIndex(new[] { "IdPoster" }, "id_poster_idx");

                    b.ToTable("category_in_posters");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInReport", b =>
                {
                    b.Property<int?>("IdCategory")
                        .HasColumnType("int")
                        .HasColumnName("id_category");

                    b.Property<int?>("IdReport")
                        .HasColumnType("int")
                        .HasColumnName("id_report");

                    b.HasIndex(new[] { "IdCategory" }, "id_category_reports_idx");

                    b.HasIndex(new[] { "IdReport" }, "id_report_idx");

                    b.ToTable("category_in_reports");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Comment", b =>
                {
                    b.Property<int>("Idcomments")
                        .HasColumnType("int")
                        .HasColumnName("idcomments");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime?>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("date_publication");

                    b.Property<int>("IdNews")
                        .HasColumnType("int")
                        .HasColumnName("id_news");

                    b.Property<string>("NameCommentator")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name_commentator");

                    b.Property<string>("TextComment")
                        .IsRequired()
                        .HasColumnType("mediumtext")
                        .HasColumnName("text_comment");

                    b.HasKey("Idcomments")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdNews" }, "_idx");

                    b.ToTable("comments");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Document", b =>
                {
                    b.Property<int>("IdDocuments")
                        .HasColumnType("int")
                        .HasColumnName("id_documents");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("date_publication");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("link");

                    b.HasKey("IdDocuments")
                        .HasName("PRIMARY");

                    b.ToTable("documents");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Efmigrationshistory", b =>
                {
                    b.Property<string>("MigrationId")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("ProductVersion")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)");

                    b.HasKey("MigrationId")
                        .HasName("PRIMARY");

                    b.ToTable("__efmigrationshistory");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Event", b =>
                {
                    b.Property<int>("IdEvents")
                        .HasColumnType("int")
                        .HasColumnName("id_events");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("date")
                        .HasColumnName("date_end");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("date")
                        .HasColumnName("date_start");

                    b.Property<string>("TextEvent")
                        .IsRequired()
                        .HasColumnType("mediumtext")
                        .HasColumnName("text_event");

                    b.Property<TimeSpan?>("TimeEnd")
                        .HasColumnType("time")
                        .HasColumnName("time_end");

                    b.Property<TimeSpan?>("TimeStart")
                        .HasColumnType("time")
                        .HasColumnName("time_start");

                    b.Property<string>("TitleEvent")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("title_event");

                    b.HasKey("IdEvents")
                        .HasName("PRIMARY");

                    b.ToTable("events");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.News", b =>
                {
                    b.Property<int>("IdNews")
                        .HasColumnType("int")
                        .HasColumnName("id_news");

                    b.Property<DateTime>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("date_publication");

                    b.Property<string>("TextNews")
                        .HasColumnType("mediumtext")
                        .HasColumnName("text_news");

                    b.Property<string>("TitleNews")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("title_news");

                    b.HasKey("IdNews")
                        .HasName("PRIMARY");

                    b.ToTable("news");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.PhotoInNews", b =>
                {
                    b.Property<int>("IdPhoto")
                        .HasColumnType("int")
                        .HasColumnName("id_photo");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<int?>("IdNews")
                        .HasColumnType("int")
                        .HasColumnName("id_news");

                    b.Property<string>("Link")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("link");

                    b.HasKey("IdPhoto")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "IdNews" }, "id_news_photo_idx");

                    b.ToTable("photo_in_news");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Poster", b =>
                {
                    b.Property<int>("IdPoster")
                        .HasColumnType("int")
                        .HasColumnName("id_poster");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime?>("DateEnd")
                        .HasColumnType("date")
                        .HasColumnName("date_end");

                    b.Property<DateTime?>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("date_publication");

                    b.Property<DateTime?>("DateStart")
                        .HasColumnType("date")
                        .HasColumnName("date_start");

                    b.Property<string>("TextPoster")
                        .HasColumnType("mediumtext")
                        .HasColumnName("text_poster");

                    b.Property<string>("TitlePoster")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("title_poster");

                    b.HasKey("IdPoster")
                        .HasName("PRIMARY");

                    b.ToTable("posters");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Report", b =>
                {
                    b.Property<int>("IdReport")
                        .HasColumnType("int")
                        .HasColumnName("id_report");

                    b.Property<DateTime?>("DateDelete")
                        .HasColumnType("date")
                        .HasColumnName("date_delete");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("date")
                        .HasColumnName("date_publication");

                    b.Property<string>("Link")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("link");

                    b.HasKey("IdReport")
                        .HasName("PRIMARY");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoriesInDocument", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.Category", "IdCategoryNavigation")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("id_category_document");

                    b.HasOne("AspNetCoreCursovaya.Models.Document", "IdDocumentNavigation")
                        .WithMany()
                        .HasForeignKey("IdDocument")
                        .HasConstraintName("id_document_docement");

                    b.Navigation("IdCategoryNavigation");

                    b.Navigation("IdDocumentNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInEvent", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.Category", "IdCategoryInEventsNavigation")
                        .WithMany()
                        .HasForeignKey("IdCategoryInEvents")
                        .HasConstraintName("id_category_in_events");

                    b.HasOne("AspNetCoreCursovaya.Models.Event", "IdEventNavigation")
                        .WithMany()
                        .HasForeignKey("IdEvent")
                        .HasConstraintName("id_event");

                    b.Navigation("IdCategoryInEventsNavigation");

                    b.Navigation("IdEventNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInNews", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.Category", "IdCategoryNavigation")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("id_category")
                        .IsRequired();

                    b.HasOne("AspNetCoreCursovaya.Models.News", "IdNewsNavigation")
                        .WithMany()
                        .HasForeignKey("IdNews")
                        .HasConstraintName("id_news_category")
                        .IsRequired();

                    b.Navigation("IdCategoryNavigation");

                    b.Navigation("IdNewsNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInPoster", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.Category", "IdCategoryNavigation")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("id_category_posters");

                    b.HasOne("AspNetCoreCursovaya.Models.Poster", "IdPosterNavigation")
                        .WithMany()
                        .HasForeignKey("IdPoster")
                        .HasConstraintName("id_poster");

                    b.Navigation("IdCategoryNavigation");

                    b.Navigation("IdPosterNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.CategoryInReport", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.Category", "IdCategoryNavigation")
                        .WithMany()
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("id_category_reports");

                    b.HasOne("AspNetCoreCursovaya.Models.Report", "IdReportNavigation")
                        .WithMany()
                        .HasForeignKey("IdReport")
                        .HasConstraintName("id_report");

                    b.Navigation("IdCategoryNavigation");

                    b.Navigation("IdReportNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.Comment", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.News", "IdNewsNavigation")
                        .WithMany("Comments")
                        .HasForeignKey("IdNews")
                        .HasConstraintName("id_news")
                        .IsRequired();

                    b.Navigation("IdNewsNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.PhotoInNews", b =>
                {
                    b.HasOne("AspNetCoreCursovaya.Models.News", "IdNewsNavigation")
                        .WithMany("PhotoInNews")
                        .HasForeignKey("IdNews")
                        .HasConstraintName("id_news_photo");

                    b.Navigation("IdNewsNavigation");
                });

            modelBuilder.Entity("AspNetCoreCursovaya.Models.News", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PhotoInNews");
                });
#pragma warning restore 612, 618
        }
    }
}
