using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoffeeLocator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToCoffeeShop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CoffeeShops",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CoffeeShops");
        }
    }
}
