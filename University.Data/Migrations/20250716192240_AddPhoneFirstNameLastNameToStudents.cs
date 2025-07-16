using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhoneFirstNameLastNameToStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Student",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Student",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }
    }
}
