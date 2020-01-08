using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
	public partial class Init : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "SimpleEntities",
				columns: table => new
				{
					id = table.Column<long>(type: "BIGINT", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					data = table.Column<string>(type: "CHAR(1000)", maxLength: 1000, nullable: false),
					date = table.Column<DateTime>(type: "DATE", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SimpleEntities", x => x.id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "SimpleEntities");
		}
	}
}
