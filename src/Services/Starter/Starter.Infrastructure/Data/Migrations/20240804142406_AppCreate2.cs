using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AppCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CostPrice",
                table: "ProductDMs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductDMs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "SellingPrice",
                table: "ProductDMs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostPrice",
                table: "ProductDMs");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductDMs");

            migrationBuilder.DropColumn(
                name: "SellingPrice",
                table: "ProductDMs");
        }
    }
}
