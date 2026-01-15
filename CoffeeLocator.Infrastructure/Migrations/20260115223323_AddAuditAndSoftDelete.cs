using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeLocator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditAndSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CoffeeShops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CoffeeShops",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "CoffeeShops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CoffeeShops",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "CoffeeShops",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "CoffeeShops",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CoffeeShopId",
                table: "Reviews",
                column: "CoffeeShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_CoffeeShops_CoffeeShopId",
                table: "Reviews",
                column: "CoffeeShopId",
                principalTable: "CoffeeShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_CoffeeShops_CoffeeShopId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CoffeeShopId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CoffeeShops");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CoffeeShops");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "CoffeeShops");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CoffeeShops");

            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "CoffeeShops");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "CoffeeShops");
        }
    }
}
