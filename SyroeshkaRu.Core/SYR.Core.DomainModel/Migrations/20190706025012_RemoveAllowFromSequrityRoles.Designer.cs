﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SYR.Core.DomainModel;

namespace SYR.Core.DomainModel.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20190706025012_RemoveAllowFromSequrityRoles")]
    partial class RemoveAllowFromSequrityRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Categories", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Keywords");

                    b.Property<int>("Level");

                    b.Property<Guid?>("ParentId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.CategoriesProducts", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<Guid>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("CategoriesProducts");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Items", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreateDate");

                    b.Property<Guid?>("ProductId");

                    b.Property<int?>("ShelfLife");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Products", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNew");

                    b.Property<string>("Keywords");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ReferenceId");

                    b.HasKey("Id");

                    b.HasIndex("ReferenceId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.References", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("References");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Storages", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsDefault");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.StoragesCategories", b =>
                {
                    b.Property<Guid>("StorageId");

                    b.Property<Guid>("CategoryId");

                    b.HasKey("StorageId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("StoragesCategories");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.StoragesProducts", b =>
                {
                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("StorageId");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Quantity");

                    b.HasKey("ProductId", "StorageId");

                    b.HasIndex("StorageId");

                    b.ToTable("StoragesProducts");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.History", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DateIn");

                    b.Property<Guid>("ItemId");

                    b.Property<int>("ItemType");

                    b.Property<int>("OperationType");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("History");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("Level");

                    b.Property<string>("Name");

                    b.Property<Guid?>("ParentId");

                    b.Property<Guid>("SequrityId");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.SequrityProfiles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("SequrityProfiles");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.SequrityRoles", b =>
                {
                    b.Property<Guid>("SequrityProfileId");

                    b.Property<string>("RoleId");

                    b.HasKey("SequrityProfileId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("SequrityRoles");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecondName");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.Roles", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<string>("Content");

                    b.HasDiscriminator().HasValue("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.System.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.System.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SYR.Core.DomainModel.System.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.System.Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.CategoriesProducts", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.Client.Categories", "Category")
                        .WithMany("CategoriesProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SYR.Core.DomainModel.Client.Products", "Product")
                        .WithMany("CategoriesProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Items", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.Client.Products", "Product")
                        .WithMany("Items")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.Products", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.Client.References", "Reference")
                        .WithMany()
                        .HasForeignKey("ReferenceId");
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.StoragesCategories", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.Client.Categories", "Category")
                        .WithMany("StoragesCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SYR.Core.DomainModel.Client.Storages", "Storage")
                        .WithMany("Categories")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SYR.Core.DomainModel.Client.StoragesProducts", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.Client.Products", "Product")
                        .WithMany("StoragesProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SYR.Core.DomainModel.Client.Storages", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SYR.Core.DomainModel.System.SequrityRoles", b =>
                {
                    b.HasOne("SYR.Core.DomainModel.System.Roles", "Roles")
                        .WithMany("SequrityRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SYR.Core.DomainModel.System.SequrityProfiles", "SequrityProfile")
                        .WithMany("SequrityRoles")
                        .HasForeignKey("SequrityProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
