using Microsoft.EntityFrameworkCore.Migrations;

namespace IvarsSykkelsjappe.Migrations
{
    public partial class BookingsAndAssistancesAdjusted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_Bookings_BookingId",
                table: "Assistances");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_BookingId",
                table: "Assistances");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Assistances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Assistances",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_BookingId",
                table: "Assistances",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_Bookings_BookingId",
                table: "Assistances",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
