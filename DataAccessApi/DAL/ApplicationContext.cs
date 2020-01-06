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

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
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
