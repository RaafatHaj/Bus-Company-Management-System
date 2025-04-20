using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelCompany.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEmaailGenderToResrevationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonEmail",
                table: "Reservations",
                type: "nvarchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PersonGendor",
                table: "Reservations",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonEmail",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PersonGendor",
                table: "Reservations");
        }
    }
}
