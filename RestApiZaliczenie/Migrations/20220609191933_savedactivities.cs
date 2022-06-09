using Microsoft.EntityFrameworkCore.Migrations;

namespace RestApiZaliczenie.Migrations
{
    public partial class savedactivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavedActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavedActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SavedActivities_Activities_Name",
                        column: x => x.Name,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavedActivities_Name",
                table: "SavedActivities",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavedActivities");
        }
    }
}
