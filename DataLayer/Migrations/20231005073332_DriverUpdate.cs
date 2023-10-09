using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DriverUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Driver_Email_PhoneNumber",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Driver");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "Driver",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Driver_Email_Mobile",
                table: "Driver",
                columns: new[] { "Email", "Mobile" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Driver_Email_Mobile",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "Driver");

            migrationBuilder.AddColumn<int>(
                name: "PhoneNumber",
                table: "Driver",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Driver_Email_PhoneNumber",
                table: "Driver",
                columns: new[] { "Email", "PhoneNumber" });
        }
    }
}
