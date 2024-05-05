using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.DAL.Migrations
{
    public partial class StatisticInHere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VisitorCount",
                table: "PageSettings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorCount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropColumn(
                name: "VisitorCount",
                table: "PageSettings");
        }
    }
}
