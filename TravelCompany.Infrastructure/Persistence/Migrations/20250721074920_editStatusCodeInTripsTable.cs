using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class editStatusCodeInTripsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "ArrivedStationOrder",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivedStationOrder",
                table: "Trips");

            migrationBuilder.AddColumn<long>(
                name: "StatusCode",
                table: "Trips",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
