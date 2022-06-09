using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApiZaliczenie.Migrations
{
    public partial class savedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SavedActivities_Activities_Name",
                table: "SavedActivities");

            migrationBuilder.DropIndex(
                name: "IX_SavedActivities_Name",
                table: "SavedActivities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SavedActivities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "SavedActivities",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SavedActivities_Name",
                table: "SavedActivities",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_SavedActivities_Activities_Name",
                table: "SavedActivities",
                column: "Name",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
