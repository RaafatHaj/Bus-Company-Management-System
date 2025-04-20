using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class setEstimatedTimeAsNullableInRoutesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoutesEstimatedTime",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "EstimatedTime",
                table: "Routes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstimatedTime",
                table: "Routes");

            migrationBuilder.AddColumn<int>(
                name: "RoutesEstimatedTime",
                table: "Routes",
                type: "int",
                nullable: true);
        }
    }
}
