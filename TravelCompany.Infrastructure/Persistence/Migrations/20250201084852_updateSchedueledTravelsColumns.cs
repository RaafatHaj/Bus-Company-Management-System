using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateSchedueledTravelsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "ScheduledTravels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Seats",
                table: "ScheduledTravels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TravelTime",
                table: "ScheduledTravels",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledTravels_RouteId",
                table: "ScheduledTravels",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledTravels_Routes_RouteId",
                table: "ScheduledTravels",
                column: "RouteId",
                principalTable: "Routes",
                principalColumn: "RouteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledTravels_Routes_RouteId",
                table: "ScheduledTravels");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledTravels_RouteId",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "ScheduledTravels");

            migrationBuilder.DropColumn(
                name: "TravelTime",
                table: "ScheduledTravels");
        }
    }
}
