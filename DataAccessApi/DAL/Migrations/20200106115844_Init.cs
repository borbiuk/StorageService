using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
	public partial class Init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "base",
				columns: table => new
				{
					id = table.Column<long>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					data = table.Column<string>(maxLength: 1000, nullable: true),
					date = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_base", x => x.id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_base_date",
				table: "base",
				column: "date");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "base");
		}
	}
}
