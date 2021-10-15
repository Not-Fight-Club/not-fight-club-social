using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SocialApi_Models.EfModels;

#nullable disable

namespace SocialApi_Data
{
    public partial class SocialDBContext : DbContext
    {
        public SocialDBContext()
        {
        }

        public SocialDBContext(DbContextOptions<SocialDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SocialDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Comment1)
                    .HasMaxLength(1000)
                    .HasColumnName("Comment");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("UserName");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ParentcommentNavigation)
                    .WithMany(p => p.InverseParentcommentNavigation)
                    .HasForeignKey(d => d.Parentcomment)
                    .HasConstraintName("FK__Comment__Parentc__24927208");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.ToTable("Friend");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
