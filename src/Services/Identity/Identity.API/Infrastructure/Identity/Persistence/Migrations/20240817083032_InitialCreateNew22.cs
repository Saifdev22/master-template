using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.API.Infrastructure.Identity.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateNew22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "AspNetRoles");
        }
    }
}
