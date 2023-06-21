using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class MyQuranContext : DbContext
    {
        public MyQuranContext()
        {
        }

        public MyQuranContext(DbContextOptions<MyQuranContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ayah> Ayahs { get; set; }
        public virtual DbSet<JuzDetail> JuzDetails { get; set; }
        public virtual DbSet<JuzHeader> JuzHeaders { get; set; }
        public virtual DbSet<Surah> Surahs { get; set; }
        public virtual DbSet<Tafsir> Tafsirs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("Server=.\\sqlexpress; Database=MyQuran; Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer(config)
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ayah>(entity =>
            {
                entity.ToTable("Ayah");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.AyahId).HasColumnName("AyahID");

                entity.Property(e => e.ReadIndo)
                    .IsRequired();
                //.HasColumnType("ntext");

                entity.Property(e => e.ReadText)
                    .IsRequired();
                    //.HasColumnType("ntext");

                entity.Property(e => e.SurahId).HasColumnName("SurahID");

                entity.Property(e => e.TranslateIndo)
                    .IsRequired();
                    //.HasColumnType("ntext");

                entity.HasOne(d => d.Tafsir)
                    .WithOne(p => p.Ayah)
                    .HasForeignKey<Ayah>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ayah_Tafsir");

                entity.HasOne(d => d.Surah)
                    .WithMany(p => p.Ayahs)
                    .HasForeignKey(d => d.SurahId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ayah_Surah");
            });

            modelBuilder.Entity<JuzDetail>(entity =>
            {
                entity.ToTable("JuzDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AyahId).HasColumnName("AyahID");

                entity.Property(e => e.JuzId).HasColumnName("JuzID");

                entity.Property(e => e.SurahId).HasColumnName("SurahID");

                entity.HasOne(d => d.Juz)
                    .WithMany(p => p.JuzDetails)
                    .HasForeignKey(d => d.JuzId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_JuzDetail_JuzHeader");
            });

            modelBuilder.Entity<JuzHeader>(entity =>
            {
                entity.ToTable("JuzHeader");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.AyahIdend).HasColumnName("AyahIDEnd");

                entity.Property(e => e.AyahIdstart).HasColumnName("AyahIDStart");

                entity.Property(e => e.SurahIdend).HasColumnName("SurahIDEnd");

                entity.Property(e => e.SurahIdstart).HasColumnName("SurahIDStart");

                entity.Property(e => e.SurahNameEnd)
                    .IsRequired();
                    //.HasColumnType("ntext");

                entity.Property(e => e.SurahNameStart)
                    .IsRequired();
                    //.HasColumnType("ntext");
            });

            modelBuilder.Entity<Surah>(entity =>
            {
                entity.ToTable("Surah");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired();
                //.HasColumnType("ntext");

                entity.Property(e => e.NameLatin)
                    .IsRequired();
                    //.HasColumnType("ntext");

                entity.Property(e => e.TranslateIndo)
                    .IsRequired();
                    //.HasColumnType("ntext");
            });

            modelBuilder.Entity<Tafsir>(entity =>
            {
                entity.ToTable("Tafsir");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AyahId).HasColumnName("AyahID");

                entity.Property(e => e.Kemenag)
                    .IsRequired();
                    //.HasColumnType("ntext");

                entity.Property(e => e.SurahId).HasColumnName("SurahID");

                entity.HasOne(d => d.Surah)
                    .WithMany(p => p.Tafsirs)
                    .HasForeignKey(d => d.SurahId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tafsir_Surah");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
