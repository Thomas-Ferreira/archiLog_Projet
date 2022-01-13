using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Archi.api.Migrations
{
    public partial class AddDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Pizza",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Pizza",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Customers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Customers");
        }
    }
}
