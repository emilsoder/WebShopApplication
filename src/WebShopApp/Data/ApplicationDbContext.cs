using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebShopApp.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<string>, string>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WebShopDatabaseVersionTwo;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryID)
                    .HasName("PK__Categori__19093A2B990B1255");

                entity.Property(e => e.CategoryID).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerID)
                    .HasName("PK__Customer__A4AE64B8DE45CF1D");

                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.Property(e => e.OrderDetailsID)
                    .HasColumnName("OrderDetailsID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductID).HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice).HasColumnType($"decimal(18,2)");

                entity.HasOne(d => d.OrderDetailsNavigation)
                    .WithOne(p => p.OrderDetails)
                    .HasForeignKey<OrderDetails>(d => d.OrderDetailsID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__OrderDeta__Order__7E37BEF6");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK__OrderDeta__Produ__7F2BE32F");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderID)
                    .HasName("PK__Orders__C3905BAFFFDAB007");

                entity.Property(e => e.OrderID).HasColumnName("OrderID");

                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.Property(e => e.Delivery).HasMaxLength(50);

                entity.Property(e => e.Discount).HasColumnType($"decimal(18,2)");

                entity.Property(e => e.NetPrice).HasColumnType($"decimal(18,2)");

                entity.Property(e => e.OrderDate).HasColumnType("smalldatetime");

                entity.Property(e => e.TotalPrice).HasColumnType($"decimal(18,2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrdersNavigation)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK__Orders__Customer__7B5B524B");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductID)
                    .HasName("PK__Products__B40CC6EDAC58B13D");

                entity.Property(e => e.ProductID).HasColumnName("ProductID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Ingredients).HasMaxLength(500);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitPrice).HasColumnType($"decimal(18,2)");
            });

            modelBuilder.Entity<ShoppingCartDetails>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ProductID).HasColumnName("ProductID");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.Property(e => e.ShoppingCartID).HasColumnName("ShoppingCartID");

                entity.Property(e => e.UnitPrice).HasColumnType($"decimal(18,2)");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ShoppingCartDetails)
                    .HasForeignKey(d => d.ProductID)
                    .HasConstraintName("FK__ShoppingC__Produ__5FB337D6");

                entity.HasOne(d => d.ShoppingCart)
                    .WithMany(p => p.ShoppingCartDetails)
                    .HasForeignKey(d => d.ShoppingCartID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK__ShoppingC__Shopp__60A75C0F");
            });

            modelBuilder.Entity<ShoppingCarts>(entity =>
            {
                entity.HasKey(e => e.ShoppingCartID)
                    .HasName("PK__Shopping__7A789A8453BD5A2F");

                entity.Property(e => e.ShoppingCartID).HasColumnName("ShoppingCartID");

                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ShoppingCarts)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK__ShoppingC__Custo__5CD6CB2B");
            });

            modelBuilder.Entity<ApplicationUser>(e =>
            {
                e.ToTable("Users").HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("UserId");
            });

            modelBuilder.Entity<IdentityRole<string>>(e =>
            {
                e.ToTable("Roles").HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(e =>
            {
                e.ToTable("RoleClaim").HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("RoleClaimId");
                e.Property(x => x.RoleId).HasColumnName("RoleId");
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(m => new { m.UserId, m.RoleId });
            modelBuilder.Entity<IdentityUserLogin<string>>(e =>
            {
                e.ToTable("UserLogin").HasKey(x => x.UserId);
                e.Property(x => x.UserId).HasColumnName("UserId");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(e =>
            {
                e.ToTable("UserClaims").HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("ClaimId");
                e.Property(x => x.UserId).HasColumnName("UserId");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(e =>
            {
                e.ToTable("UserTokens").HasKey(x => x.UserId);
                e.Property(x => x.UserId).HasColumnName("UserId");
            });
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        public virtual DbSet<ShoppingCartDetails> ShoppingCartDetails { get; set; }
        public virtual DbSet<ShoppingCarts> ShoppingCarts { get; set; }
    }
}


// Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=WebShopAppDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
