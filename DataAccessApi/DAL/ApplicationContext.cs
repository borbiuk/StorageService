﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ApplicationContext : DbContext
	{
		/// <summary>
		/// Simple entities storage.
		/// </summary>
		public DbSet<SimpleEntity> SimpleEntities { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<SimpleEntity>(entity =>
			{
				entity.HasKey(_ => _.Id);
				entity.Property(_ => _.Id)
					.HasColumnName("id")
					.HasColumnType("BIGINT")
					.ValueGeneratedOnAdd()
					.IsRequired();

				entity.Property(_ => _.Date)
					.HasColumnName("date")
					.HasColumnType("DATETIME")
					.IsRequired();

				entity.Property(_ => _.Data)
					.HasColumnName("data")
					.HasColumnType("CHAR")
					.HasMaxLength(1_000)
					.IsRequired();
			});
		}
	}
}
