using Microsoft.EntityFrameworkCore.Migrations;

namespace IvarsSykkelsjappe.Migrations
{
    public partial class RemovedRelationBookingAssistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Assistances_AssistanceId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_AssistanceId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "AssistanceId",
                table: "Bookings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssistanceId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_AssistanceId",
                table: "Bookings",
                column: "AssistanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Assistances_AssistanceId",
                table: "Bookings",
                column: "AssistanceId",
                principalTable: "Assistances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
