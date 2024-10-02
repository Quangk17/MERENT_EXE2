using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class urlproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URLCertain",
                table: "Products",
                newName: "URLSide");

            migrationBuilder.AddColumn<string>(
                name: "URLCenter",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLCenter",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "URLSide",
                table: "Products",
                newName: "URLCertain");
        }
    }
}
