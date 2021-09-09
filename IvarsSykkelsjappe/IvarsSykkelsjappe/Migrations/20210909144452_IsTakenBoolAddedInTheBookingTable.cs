using Microsoft.EntityFrameworkCore.Migrations;

namespace IvarsSykkelsjappe.Migrations
{
    public partial class IsTakenBoolAddedInTheBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTaken",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTaken",
                table: "Bookings");
        }
    }
}
