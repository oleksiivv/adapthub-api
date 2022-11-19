using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adapthub_api.Migrations
{
    public partial class ChangesForStatusFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Vacancies",
                newName: "_status");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "JobRequests",
                newName: "_status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_status",
                table: "Vacancies",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "_status",
                table: "JobRequests",
                newName: "Status");
        }
    }
}
