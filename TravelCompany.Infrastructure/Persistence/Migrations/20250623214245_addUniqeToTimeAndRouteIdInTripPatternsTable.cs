using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addUniqeToTimeAndRouteIdInTripPatternsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripPatterns_RouteId",
                table: "TripPatterns");

            migrationBuilder.CreateIndex(
                name: "IX_TripPatterns_RouteId_Time",
                table: "TripPatterns",
                columns: new[] { "RouteId", "Time" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripPatterns_RouteId_Time",
                table: "TripPatterns");

            migrationBuilder.CreateIndex(
                name: "IX_TripPatterns_RouteId",
                table: "TripPatterns",
                column: "RouteId");
        }
    }
}
