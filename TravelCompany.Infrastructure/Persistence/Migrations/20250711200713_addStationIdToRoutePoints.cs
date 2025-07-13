using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addStationIdToRoutePoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "RoutePoints",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_StationId",
                table: "RoutePoints",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoutePoints_Stations_StationId",
                table: "RoutePoints",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutePoints_Stations_StationId",
                table: "RoutePoints");

            migrationBuilder.DropIndex(
                name: "IX_RoutePoints_StationId",
                table: "RoutePoints");

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "RoutePoints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
