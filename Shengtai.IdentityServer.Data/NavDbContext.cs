using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shengtai.IdentityServer.Data
{
    public partial class NavDbContext : DbContext
    {
        public NavDbContext()
        {
        }

        public NavDbContext(DbContextOptions<NavDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Menu> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese (Traditional)_Taiwan.950");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.BadgeClass).HasMaxLength(16);

                entity.Property(e => e.BadgeText).HasMaxLength(4);

                entity.Property(e => e.Icon).HasMaxLength(32);

                entity.Property(e => e.Small).HasMaxLength(15);

                entity.Property(e => e.Text).HasMaxLength(24);

                entity.Property(e => e.Url).HasMaxLength(48);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Menu_ParentId_fkey1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
