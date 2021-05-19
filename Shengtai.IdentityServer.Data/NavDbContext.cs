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
        public virtual DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese (Traditional)_Taiwan.950");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasIndex(e => new { e.ParentId, e.Text, e.Small }, "Menu_ParentId_Text_Small_key")
                    .IsUnique();

                entity.Property(e => e.Icon).HasMaxLength(32);

                entity.Property(e => e.Small).HasMaxLength(15);

                entity.Property(e => e.Text).HasMaxLength(24);

                entity.Property(e => e.Url).HasMaxLength(48);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Menu_ParentId_fkey");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.UserId })
                    .HasName("MenuItem_pkey");

                entity.ToTable("MenuItem");

                entity.Property(e => e.UserId).HasMaxLength(36);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("MenuItem_MenuId_fkey1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
