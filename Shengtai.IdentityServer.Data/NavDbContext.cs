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
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<MenuUser> MenuUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese (Traditional)_Taiwan.950");

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.HasIndex(e => new { e.ParentId, e.Text, e.Small }, "Menu_ParentId_Text_Small_key1")
                    .IsUnique();

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

            modelBuilder.Entity<MenuRole>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.RoleId })
                    .HasName("MenuRole_pkey");

                entity.ToTable("MenuRole");

                entity.Property(e => e.RoleId).HasMaxLength(36);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuRoles)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("MenuRole_MenuId_fkey");
            });

            modelBuilder.Entity<MenuUser>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.UserId })
                    .HasName("MenuUser_pkey");

                entity.ToTable("MenuUser");

                entity.Property(e => e.UserId).HasMaxLength(36);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuUsers)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("MenuUser_MenuId_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
