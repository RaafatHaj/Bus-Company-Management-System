using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addInhertanceToReservaeionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedById",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Reservations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TripDepartureDateTime",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CreatedById",
                table: "Reservations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LastUpdatedById",
                table: "Reservations",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StationId",
                table: "AspNetUsers",
                column: "StationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Stations_StationId",
                table: "AspNetUsers",
                column: "StationId",
                principalTable: "Stations",
                principalColumn: "StationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_CreatedById",
                table: "Reservations",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_LastUpdatedById",
                table: "Reservations",
                column: "LastUpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Stations_StationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_CreatedById",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_LastUpdatedById",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CreatedById",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_LastUpdatedById",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LastUpdatedById",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TripDepartureDateTime",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "StationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
