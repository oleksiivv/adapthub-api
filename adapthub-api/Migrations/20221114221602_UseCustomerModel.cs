using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adapthub_api.Migrations
{
    public partial class UseCustomerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Users_UserId",
                table: "JobRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Moderators_Users_UserId",
                table: "Moderators");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Moderators_UserId",
                table: "Moderators");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_UserId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Moderators");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobRequests");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Moderators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Moderators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_CustomerId",
                table: "JobRequests",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Users_CustomerId",
                table: "JobRequests",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequests_Users_CustomerId",
                table: "JobRequests");

            migrationBuilder.DropIndex(
                name: "IX_JobRequests_CustomerId",
                table: "JobRequests");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Moderators");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Moderators");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "JobRequests");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Moderators",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Moderators_UserId",
                table: "Moderators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_UserId",
                table: "JobRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequests_Users_UserId",
                table: "JobRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moderators_Users_UserId",
                table: "Moderators",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Users_UserId",
                table: "Organizations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
