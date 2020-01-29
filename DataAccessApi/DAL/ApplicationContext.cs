using DAL.Entities;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ApplicationContext : DbContext
	{
		public DbSet<UserEntity> UsersEntities { get; set; }

		public DbSet<SoftEntity> SoftwareEntities { get; set; }

		public DbSet<UserSoftEntity> UserSoftEntities { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<UserEntity>(entity =>
			{
				entity.HasKey(_ => _.Id);

				entity.Property(_ => _.Id)
					.HasColumnName("id")
					.HasColumnType("BIGINT")
					.ValueGeneratedOnAdd()
					.IsRequired();

				entity.Property(_ => _.Name)
					.HasColumnName("name")
					.HasColumnType("CHAR(20)")
					.HasMaxLength(20)
					.IsRequired();
			});

			modelBuilder.Entity<SoftEntity>(entity =>
			{
				entity.HasKey(_ => _.Id);

				entity.Property(_ => _.Id)
					.HasColumnName("id")
					.HasColumnType("BIGINT")
					.ValueGeneratedOnAdd()
					.IsRequired();

				entity.Property(_ => _.Name)
					.HasColumnName("name")
					.HasColumnType("CHAR(30)")
					.HasMaxLength(30)
					.IsRequired();
			});

			modelBuilder.Entity<UserSoftEntity>(entity =>
			{
				entity.HasKey(_ => _.Id);

				entity.HasIndex(_ => new {_.UserId, _.SoftId})
					.IsUnique();

				entity.Property(_ => _.Id)
					.HasColumnName("id")
					.HasColumnType("BIGINT")
					.ValueGeneratedOnAdd()
					.IsRequired();

				entity.HasOne(_ => _.User)
					.WithMany(_ => _.UserSoftEntities)
					.HasForeignKey(_ => _.UserId);

				entity.HasOne(_ => _.Soft)
					.WithMany(_ => _.UserSoftEntities)
					.HasForeignKey(_ => _.SoftId);
			});
		}
	}
}
