using System;
using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SYR.Core.DomainModel.Client;
using SYR.Core.DomainModel.System;

namespace SYR.Core.DomainModel {
	public sealed class ModelContext : IdentityDbContext<Users> {
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
				optionsBuilder.UseSqlServer(
					"Server=.\\SQLEXPRESS;Database=SYRDB;Trusted_Connection=True;MultipleActiveResultSets=true");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region StoragesProducts
			
			modelBuilder.Entity<StoragesProducts>()
				.HasKey(s => new {s.ProductId, s.StorageId});

			modelBuilder.Entity<StoragesProducts>()
				.HasOne(sp => sp.Product)
				.WithMany(p => p.StoragesProducts)
				.HasForeignKey(sp => sp.ProductId);

			modelBuilder.Entity<StoragesProducts>()
				.HasOne(sp => sp.Storage)
				.WithMany(s => s.Products)
				.HasForeignKey(sp => sp.StorageId);

			#endregion

			#region StoragesCategories

			modelBuilder.Entity<StoragesCategories>()
				.HasKey(s => new {s.StorageId, s.CategoryId});

			modelBuilder.Entity<StoragesCategories>()
				.HasOne(sc => sc.Category)
				.WithMany(c => c.StoragesCategories)
				.HasForeignKey(sc => sc.CategoryId);

			modelBuilder.Entity<StoragesCategories>()
				.HasOne(sc => sc.Storage)
				.WithMany(s => s.Categories)
				.HasForeignKey(sc => sc.StorageId);

			#endregion

			#region SequrityRoles

			modelBuilder.Entity<SequrityRoles>()
				.HasKey(c => new {c.SequrityProfileId, c.RoleId});

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

		#region DbSet

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<Products> Products { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<Storages> Storages { get; set; }
		public DbSet<Categories> Categories { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<StoragesProducts> StoragesProducts { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<StoragesCategories> StoragesCategories { get; set; }
		public DbSet<CategoriesProducts> CategoriesProducts { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<Menu> Menu { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<History> History { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public new DbSet<Users> Users { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public new DbSet<Roles> Roles { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<SequrityProfiles> SequrityProfiles { get; set; }

		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public DbSet<SequrityRoles> SequrityRoles { get; set; }

		#endregion
	}
}