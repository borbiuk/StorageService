using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class AppContext : DbContext
	{
		/// <summary>
		/// Simple entities storage.
		/// </summary>
		public DbSet<EntityBase> Entities { get; set; }

		public AppContext()
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=mydata;Trusted_Connection=True;");
		}
	}
}
