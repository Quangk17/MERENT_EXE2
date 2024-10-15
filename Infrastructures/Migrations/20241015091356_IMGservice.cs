using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class IMGservice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "ComboOfProducts");

            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "Services");

            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "ComboOfProducts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
