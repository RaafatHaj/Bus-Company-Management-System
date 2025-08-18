using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deleteLateTimeFromeActiveAndCompletedTripTracksTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LateMinutes",
                table: "CompletedTripTracks");

            migrationBuilder.DropColumn(
                name: "LateMinutes",
                table: "ActiveTripTracks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LateMinutes",
                table: "CompletedTripTracks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LateMinutes",
                table: "ActiveTripTracks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
