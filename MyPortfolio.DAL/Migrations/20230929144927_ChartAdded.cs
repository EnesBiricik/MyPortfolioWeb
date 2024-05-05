using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.DAL.Migrations
{
    public partial class ChartAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClickCount",
                table: "SocialMedias",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClickCount",
                table: "SocialMedias");
        }
    }
}
