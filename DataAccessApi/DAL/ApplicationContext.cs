using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ApplicationContext : DbContext
	{
		/// <summary>
		/// Simple entities storage.
		/// </summary>
		public DbSet<BaseEntity> Entities { get; set; }

		public ApplicationContext()
		{
			//Create db automaticly
			//Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mydata;Trusted_Connection=True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<BaseEntity>(entity =>
			{
				entity.HasKey(_ => _.Id);
				entity.Property(_ => _.Id).ValueGeneratedOnAdd();
				entity.HasIndex(_ => _.Date);
			});
		}
	}
}
