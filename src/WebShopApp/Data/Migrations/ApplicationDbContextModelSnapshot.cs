using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebShopApp.Models;

namespace WebShopApp.Data
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleId");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RoleClaimId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("IdentityRole<string>Id");

                    b.Property<string>("RoleId")
                        .HasColumnName("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("IdentityRole<string>Id");

                    b.ToTable("RoleClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClaimId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("ProviderKey");

                    b.HasKey("UserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserLogin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("IdentityRole<string>Id");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("IdentityRole<string>Id");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("WebShopApp.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UserId");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebShopApp.Models.Categories", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryID");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("CategoryNumber");

                    b.HasKey("CategoryID")
                        .HasName("PK__Categori__19093A2B990B1255");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebShopApp.Models.Customers", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CustomerID");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("Orders");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CustomerID")
                        .HasName("PK__Customer__A4AE64B8DE45CF1D");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WebShopApp.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderDetailsID");

                    b.Property<int?>("ProductID")
                        .HasColumnName("ProductID");

                    b.Property<int?>("Quantity");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderDetailsID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("WebShopApp.Models.Orders", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderID");

                    b.Property<int?>("CustomerID")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Delivery")
                        .HasMaxLength(50);

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("NetPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("smalldatetime");

                    b.Property<int?>("ProductsQty");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderID")
                        .HasName("PK__Orders__C3905BAFFFDAB007");

                    b.HasIndex("CustomerID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebShopApp.Models.Products", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID");

                    b.Property<int?>("CategoryNumber");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Ingredients")
                        .HasMaxLength(500);

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID")
                        .HasName("PK__Products__B40CC6EDAC58B13D");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebShopApp.Models.ShoppingCartDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID");

                    b.Property<int?>("ProductID")
                        .HasColumnName("ProductID");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50);

                    b.Property<int?>("Quantity");

                    b.Property<int>("ShoppingCartID")
                        .HasColumnName("ShoppingCartID");

                    b.Property<decimal?>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.HasIndex("ProductID");

                    b.HasIndex("ShoppingCartID");

                    b.ToTable("ShoppingCartDetails");
                });

            modelBuilder.Entity("WebShopApp.Models.ShoppingCarts", b =>
                {
                    b.Property<int>("ShoppingCartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShoppingCartID");

                    b.Property<int?>("CustomerID")
                        .HasColumnName("CustomerID");

                    b.HasKey("ShoppingCartID")
                        .HasName("PK__Shopping__7A789A8453BD5A2F");

                    b.HasIndex("CustomerID");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>")
                        .WithMany("Claims")
                        .HasForeignKey("IdentityRole<string>Id");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("WebShopApp.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WebShopApp.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("WebShopApp.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole<string>")
                        .WithMany("Users")
                        .HasForeignKey("IdentityRole<string>Id");
                });

            modelBuilder.Entity("WebShopApp.Models.OrderDetails", b =>
                {
                    b.HasOne("WebShopApp.Models.Orders", "OrderDetailsNavigation")
                        .WithOne("OrderDetails")
                        .HasForeignKey("WebShopApp.Models.OrderDetails", "OrderDetailsID");

                    b.HasOne("WebShopApp.Models.Products", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductID");
                });

            modelBuilder.Entity("WebShopApp.Models.Orders", b =>
                {
                    b.HasOne("WebShopApp.Models.Customers", "Customer")
                        .WithMany("OrdersNavigation")
                        .HasForeignKey("CustomerID")
                        .HasConstraintName("FK__Orders__Customer__7B5B524B");
                });

            modelBuilder.Entity("WebShopApp.Models.ShoppingCartDetails", b =>
                {
                    b.HasOne("WebShopApp.Models.Products", "Product")
                        .WithMany("ShoppingCartDetails")
                        .HasForeignKey("ProductID")
                        .HasConstraintName("FK__ShoppingC__Produ__5FB337D6");

                    b.HasOne("WebShopApp.Models.ShoppingCarts", "ShoppingCart")
                        .WithMany("ShoppingCartDetails")
                        .HasForeignKey("ShoppingCartID");
                });

            modelBuilder.Entity("WebShopApp.Models.ShoppingCarts", b =>
                {
                    b.HasOne("WebShopApp.Models.Customers", "Customer")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("CustomerID")
                        .HasConstraintName("FK__ShoppingC__Custo__5CD6CB2B");
                });
        }
    }
}
