using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addTripMovingToTripsAndStationOrderToRoutePoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVehicleMoving",
                table: "Trips",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StationId",
                table: "RoutePoints",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVehicleMoving",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "StationId",
                table: "RoutePoints");
        }
    }
}
