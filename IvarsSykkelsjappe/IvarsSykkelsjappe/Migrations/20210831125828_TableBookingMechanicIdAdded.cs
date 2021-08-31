using Microsoft.EntityFrameworkCore.Migrations;

namespace IvarsSykkelsjappe.Migrations
{
    public partial class TableBookingMechanicIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MechanicId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MechanicId",
                table: "Bookings");
        }
    }
}
