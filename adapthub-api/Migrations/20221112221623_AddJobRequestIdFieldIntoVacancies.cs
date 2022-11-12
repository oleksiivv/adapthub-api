using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adapthub_api.Migrations
{
    public partial class AddJobRequestIdFieldIntoVacancies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FoundUserId",
                table: "Vacancies",
                newName: "JobRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobRequestId",
                table: "Vacancies",
                newName: "FoundUserId");
        }
    }
}
