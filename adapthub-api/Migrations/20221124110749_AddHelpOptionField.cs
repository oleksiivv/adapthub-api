using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adapthub_api.Migrations
{
    public partial class AddHelpOptionField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HelpOption",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HelpOption",
                table: "Users");
        }
    }
}
