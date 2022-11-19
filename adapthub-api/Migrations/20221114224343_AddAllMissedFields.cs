using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace adapthub_api.Migrations
{
    public partial class AddAllMissedFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Vacancies",
                newName: "Speciality");

            migrationBuilder.RenameColumn(
                name: "JobRequestId",
                table: "Vacancies",
                newName: "ChosenJobRequestId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "JobRequests",
                newName: "Speciality");

            migrationBuilder.AddColumn<int>(
                name: "MinExperience",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentAddress",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExperienceId",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IDCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EDRPOU",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Moderators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Moderators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExpectedSalary",
                table: "JobRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CustomerExperience",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PastJob = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Profession = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerExperience", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ChosenJobRequestId",
                table: "Vacancies",
                column: "ChosenJobRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ExperienceId",
                table: "Users",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CustomerExperience_ExperienceId",
                table: "Users",
                column: "ExperienceId",
                principalTable: "CustomerExperience",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_JobRequests_ChosenJobRequestId",
                table: "Vacancies",
                column: "ChosenJobRequestId",
                principalTable: "JobRequests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CustomerExperience_ExperienceId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_JobRequests_ChosenJobRequestId",
                table: "Vacancies");

            migrationBuilder.DropTable(
                name: "CustomerExperience");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_ChosenJobRequestId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Users_ExperienceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MinExperience",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "CurrentAddress",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IDCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "EDRPOU",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Moderators");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Moderators");

            migrationBuilder.DropColumn(
                name: "ExpectedSalary",
                table: "JobRequests");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Vacancies",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ChosenJobRequestId",
                table: "Vacancies",
                newName: "JobRequestId");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "JobRequests",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "JobRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
