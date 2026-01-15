using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeLocator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CoffeeShops");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Visits",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Reviews",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Comments",
                newName: "LastModifiedAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Achievements",
                newName: "LastModifiedAt");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Visits",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Visits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Comments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Comments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Achievements",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Achievements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Achievements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Achievements");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Achievements");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Visits",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Reviews",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Comments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAt",
                table: "Achievements",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CoffeeShops",
                type: "datetime2",
                nullable: true);
        }
    }
}
