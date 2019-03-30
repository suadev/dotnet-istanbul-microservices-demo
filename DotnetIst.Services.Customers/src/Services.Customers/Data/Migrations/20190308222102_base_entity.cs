using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Services.Customers.Data.Migrations
{
    public partial class base_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Basket",
                newName: "UpdateDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Customer",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "BasketItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "BasketItem");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "Basket",
                newName: "CreatedAt");
        }
    }
}
