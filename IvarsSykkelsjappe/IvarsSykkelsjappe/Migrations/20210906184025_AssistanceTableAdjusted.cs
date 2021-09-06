using Microsoft.EntityFrameworkCore.Migrations;

namespace IvarsSykkelsjappe.Migrations
{
    public partial class AssistanceTableAdjusted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_AssistancesCategories_AssistanceCategoryId",
                table: "Assistances");

            migrationBuilder.DropTable(
                name: "AssistancesCategories");

            migrationBuilder.DropIndex(
                name: "IX_Assistances_AssistanceCategoryId",
                table: "Assistances");

            migrationBuilder.DropColumn(
                name: "AssistanceCategoryId",
                table: "Assistances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssistanceCategoryId",
                table: "Assistances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AssistancesCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistancesCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assistances_AssistanceCategoryId",
                table: "Assistances",
                column: "AssistanceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_AssistancesCategories_AssistanceCategoryId",
                table: "Assistances",
                column: "AssistanceCategoryId",
                principalTable: "AssistancesCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
