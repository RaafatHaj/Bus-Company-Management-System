using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRreservationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledTravelId = table.Column<int>(type: "int", nullable: false),
                    StationAId = table.Column<int>(type: "int", nullable: false),
                    StationBId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    SeatCode = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_ScheduledTravels_ScheduledTravelId",
                        column: x => x.ScheduledTravelId,
                        principalTable: "ScheduledTravels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Stations_StationAId",
                        column: x => x.StationAId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Stations_StationBId",
                        column: x => x.StationBId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ScheduledTravelId",
                table: "Reservations",
                column: "ScheduledTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StationAId",
                table: "Reservations",
                column: "StationAId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_StationBId",
                table: "Reservations",
                column: "StationBId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");
        }
    }
}
