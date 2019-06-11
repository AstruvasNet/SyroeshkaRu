using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SYR.Core.DomainModel.Client;
using System;
using System.IO;
using SYR.Core.DomainModel.System;

namespace SYR.Core.DomainModel
{
	public sealed class ModelContext : IdentityDbContext<Users>
	{
		public DbSet<Products> Products { get; set; }
		public DbSet<Storages> Storages { get; set; }
		public DbSet<Categories> Categories { get; set; }
		public DbSet<StoragesProducts> StoragesProducts { get; set; }
		public DbSet<StoragesCategories> StoragesCategories { get; set; }
		public DbSet<CategoriesProducts> CategoriesProducts { get; set; }
		public DbSet<Menu> Menu { get; set; }
		public DbSet<History> History { get; set; }

		public new DbSet<Users> Users { get; set; }
		public new DbSet<Roles> Roles { get; set; }

		public DbSet<SequrityProfiles> SequrityProfiles { get; set; }
		public DbSet<SequrityRoles> SequrityRoles { get; set; }

		public ModelContext(DbContextOptions<ModelContext> options) : base(options)
		{
			Database.Migrate();
		}

		public ModelContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			try
			{
				var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appsettings.json").Build();
				optionsBuilder.UseSqlServer(
					builder.GetConnectionString(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")));
			}
			catch (Exception)
			{
				optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=SYRDB;Trusted_Connection=True;MultipleActiveResultSets=true");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region Склады и продукты

			modelBuilder.Entity<StoragesProducts>()
				.HasKey(s => new { s.ProductId, s.StorageId });

			modelBuilder.Entity<StoragesProducts>()
				.HasOne(sp => sp.Product)
				.WithMany(p => p.StoragesProducts)
				.HasForeignKey(sp => sp.ProductId);

			modelBuilder.Entity<StoragesProducts>()
				.HasOne(sp => sp.Storage)
				.WithMany(s => s.Products)
				.HasForeignKey(sp => sp.StorageId);

			#endregion

			#region Склады и категории

			modelBuilder.Entity<StoragesCategories>()
				.HasKey(s => new { s.StorageId, s.CategoryId });

			modelBuilder.Entity<StoragesCategories>()
				.HasOne(sc => sc.Category)
				.WithMany(c => c.StoragesCategories)
				.HasForeignKey(sc => sc.CategoryId);

			modelBuilder.Entity<StoragesCategories>()
				.HasOne(sc => sc.Storage)
				.WithMany(s => s.Categories)
				.HasForeignKey(sc => sc.StorageId);

			#endregion

			#region Категории и продукты

			modelBuilder.Entity<CategoriesProducts>()
				.HasKey(c => new { c.ProductId, c.CategoryId });

			modelBuilder.Entity<CategoriesProducts>()
				.HasOne(cp => cp.Product)
				.WithMany(p => p.CategoriesProducts)
				.HasForeignKey(cp => cp.ProductId);

			modelBuilder.Entity<CategoriesProducts>()
				.HasOne(cp => cp.Category)
				.WithMany(c => c.CategoriesProducts)
				.HasForeignKey(cp => cp.CategoryId);

			#endregion

			#region Роли и ресурсы

			modelBuilder.Entity<SequrityRoles>()
				.HasKey(c => new { c.SequrityProfileId, c.RoleId });

			modelBuilder.Entity<SequrityRoles>()
				.HasOne(rr => rr.SequrityProfile)
				.WithMany(r => r.SequrityRoles)
				.HasForeignKey(rr => rr.SequrityProfileId);

			modelBuilder.Entity<SequrityRoles>()
				.HasOne(rr => rr.Roles)
				.WithMany(r => r.SequrityRoles)
				.HasForeignKey(rr => rr.RoleId);

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}
