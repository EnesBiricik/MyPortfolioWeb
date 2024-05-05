using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPortfolio.DAL.Migrations
{
    public partial class BlogFeatureUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Blogs");
        }
    }
}
