using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class update_comboproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ComboOfProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "ComboOfProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DeleteBy",
                table: "ComboOfProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "ComboOfProducts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ComboOfProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ComboOfProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ModificationBy",
                table: "ComboOfProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModificationDate",
                table: "ComboOfProducts",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "DeleteBy",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "ModificationBy",
                table: "ComboOfProducts");

            migrationBuilder.DropColumn(
                name: "ModificationDate",
                table: "ComboOfProducts");
        }
    }
}
