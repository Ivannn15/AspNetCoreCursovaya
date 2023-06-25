using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class cursovayadbContext : DbContext
    {
        public cursovayadbContext()
        {
        } 

        public cursovayadbContext(DbContextOptions<cursovayadbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<CategoriesInDocument> CategoriesInDocuments { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryInEvent> CategoryInEvents { get; set; }
        public virtual DbSet<CategoryInNews> CategoryInNews { get; set; }
        public virtual DbSet<CategoryInPoster> CategoryInPosters { get; set; }
        public virtual DbSet<CategoryInReport> CategoryInReports { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<PhotoInNews> PhotoInNews { get; set; }
        public virtual DbSet<Poster> Posters { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<partners> partners { get; set; }
        public virtual DbSet<category_in_partners> category_in_partners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("Server=MYSQL5045.site4now.net;Database=db_a992f1_ivan123;Uid=a992f1_ivan123;Pwd=Dfyz2000;", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
            }
        } // server=localhost;user=root;password=root;database=cursovayadb;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.IdAdmin)
                    .HasName("PRIMARY");

                entity.ToTable("admin");

                entity.Property(e => e.IdAdmin)
                    .ValueGeneratedNever()
                    .HasColumnName("id_admin");

                entity.Property(e => e.HashPassword)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("hash_password");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("salt");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<CategoriesInDocument>(entity =>
            {

                entity.HasKey(e => e.IdCategoryInDocument)
                    .HasName("PRIMARY");

                entity.ToTable("categories_in_documents");

                entity.HasIndex(e => e.IdCategory, "id_category_document_idx");

                entity.HasIndex(e => e.IdDocument, "id_document_docement_idx");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdDocument).HasColumnName("id_document");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("id_category_document");

                entity.HasOne(d => d.IdDocumentNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdDocument)
                    .HasConstraintName("id_document_docement");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategories)
                    .HasName("PRIMARY");

                entity.ToTable("categories");

                entity.Property(e => e.IdCategories)
                    .ValueGeneratedNever()
                    .HasColumnName("id_categories");

                entity.Property(e => e.TitleCategory)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title_category");
            });

           

            modelBuilder.Entity<CategoryInEvent>(entity =>
            {

                entity.HasKey(e => e.id_category_event_record)
                    .HasName("PRIMARY");

                //entity.HasNoKey();

                entity.ToTable("category_in_events");

                entity.HasIndex(e => e.IdCategoryInEvents, "id_category_in_events_idx");

                entity.HasIndex(e => e.IdEvent, "id_event_idx");

                entity.Property(e => e.IdCategoryInEvents).HasColumnName("id_category_in_events");

                entity.Property(e => e.IdEvent).HasColumnName("id_event");

                entity.HasOne(d => d.IdCategoryInEventsNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategoryInEvents)
                    .HasConstraintName("id_category_in_events");

                entity.HasOne(d => d.IdEventNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdEvent)
                    .HasConstraintName("id_event");
            });

            modelBuilder.Entity<CategoryInNews>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("category_in_news");

                entity.HasIndex(e => e.IdNews, "id_category_idx");

                entity.HasIndex(e => e.IdCategory, "id_category_idx1");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdNews).HasColumnName("id_news");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_category");

                entity.HasOne(d => d.IdNewsNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdNews)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_news_category");
            });

            modelBuilder.Entity<CategoryInPoster>(entity =>
            {
                entity.HasKey(e => e.idCategoryInPoster)
                   .HasName("PRIMARY");

                //entity.HasNoKey();

                entity.ToTable("category_in_posters");

                entity.HasIndex(e => e.IdCategory, "id_category_posters_idx");

                entity.HasIndex(e => e.IdPoster, "id_poster_idx");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdPoster).HasColumnName("id_poster");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("id_category_posters");

                entity.HasOne(d => d.IdPosterNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPoster)
                    .HasConstraintName("id_poster");
            });

            modelBuilder.Entity<CategoryInReport>(entity =>
            {
                entity.HasKey(e => e.idCategoryInReport)
                    .HasName("PRIMARY");

                entity.ToTable("category_in_reports");

                entity.HasIndex(e => e.IdCategory, "id_category_reports_idx");

                entity.HasIndex(e => e.IdReport, "id_report_idx");

                entity.Property(e => e.IdCategory).HasColumnName("id_category");

                entity.Property(e => e.IdReport).HasColumnName("id_report");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("id_category_reports");

                entity.HasOne(d => d.IdReportNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdReport)
                    .HasConstraintName("id_report");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Idcomments)
                    .HasName("PRIMARY");

                entity.ToTable("comments");

                entity.HasIndex(e => e.IdNews, "_idx");

                entity.Property(e => e.Idcomments)
                    .ValueGeneratedNever()
                    .HasColumnName("idcomments");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DatePublication)
                    .HasColumnType("date")
                    .HasColumnName("date_publication");

                entity.Property(e => e.IdNews).HasColumnName("id_news");

                entity.Property(e => e.NameCommentator)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("name_commentator");

                entity.Property(e => e.TextComment)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text_comment");

                entity.HasOne(d => d.IdNewsNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdNews)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("id_news");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasKey(e => e.IdDocuments)
                    .HasName("PRIMARY");

                entity.ToTable("documents");

                entity.Property(e => e.IdDocuments)
                    .ValueGeneratedNever()
                    .HasColumnName("id_documents");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DatePublication)
                    .HasColumnType("date")
                    .HasColumnName("date_publication");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("link");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.IdEvents)
                    .HasName("PRIMARY");

                entity.ToTable("events");

                entity.Property(e => e.IdEvents)
                    .ValueGeneratedNever()
                    .HasColumnName("id_events");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("date_end");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.TextEvent)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("text_event");

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("time")
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .HasColumnType("time")
                    .HasColumnName("time_start");

                entity.Property(e => e.TitleEvent)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title_event");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.IdNews)
                    .HasName("PRIMARY");

                entity.ToTable("news");

                entity.Property(e => e.IdNews)
                    .ValueGeneratedNever()
                    .HasColumnName("id_news");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DatePublication)
                    .HasColumnType("date")
                    .HasColumnName("date_publication");

                entity.Property(e => e.TextNews)
                    .HasColumnType("mediumtext")
                    .HasColumnName("text_news");

                entity.Property(e => e.TitleNews)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("title_news");
            });

            modelBuilder.Entity<PhotoInNews>(entity =>
            {
                entity.HasKey(e => e.IdPhoto)
                    .HasName("PRIMARY");

                entity.ToTable("photo_in_news");

                entity.HasIndex(e => e.IdNews, "id_news_photo_idx");

                entity.Property(e => e.IdPhoto)
                    .ValueGeneratedNever()
                    .HasColumnName("id_photo");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.IdNews).HasColumnName("id_news");

                entity.Property(e => e.Link)
                    .HasMaxLength(150)
                    .HasColumnName("link");

                entity.HasOne(d => d.IdNewsNavigation)
                    .WithMany(p => p.PhotoInNews)
                    .HasForeignKey(d => d.IdNews)
                    .HasConstraintName("id_news_photo");
            });

            modelBuilder.Entity<Poster>(entity =>
            {
                entity.HasKey(e => e.IdPoster)
                    .HasName("PRIMARY");

                entity.ToTable("posters");

                entity.Property(e => e.IdPoster)
                    .ValueGeneratedNever()
                    .HasColumnName("id_poster");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DateEnd)
                    .HasColumnType("date")
                    .HasColumnName("date_end");

                entity.Property(e => e.DatePublication)
                    .HasColumnType("date")
                    .HasColumnName("date_publication");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.TextPoster)
                    .HasColumnType("mediumtext")
                    .HasColumnName("text_poster");

                entity.Property(e => e.TitlePoster)
                    .HasMaxLength(45)
                    .HasColumnName("title_poster");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.IdReport)
                    .HasName("PRIMARY");

                entity.ToTable("reports");

                entity.Property(e => e.IdReport)
                    .ValueGeneratedNever()
                    .HasColumnName("id_report");

                entity.Property(e => e.DateDelete)
                    .HasColumnType("date")
                    .HasColumnName("date_delete");

                entity.Property(e => e.DatePublication)
                    .HasColumnType("date")
                    .HasColumnName("date_publication");

                entity.Property(e => e.Link)
                    .HasMaxLength(100)
                    .HasColumnName("link");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
