using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Starter.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProductDMAdd2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductDMs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductDMs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProductDMs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProductDMs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductDMs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductDMs");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProductDMs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProductDMs");
        }
    }
}
