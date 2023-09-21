using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_DriverId",
                table: "Ride");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Driver_DriverId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Ride_DriverId",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_UserId",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Payment_RideId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserId",
                table: "Payment");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RideId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RideId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RideId",
                table: "Driver",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ride_UserId",
                table: "Ride",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RideId",
                table: "Payment",
                column: "RideId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_RideId",
                table: "Driver",
                column: "RideId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Ride_RideId",
                table: "Driver",
                column: "RideId",
                principalTable: "Ride",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment",
                column: "RideId",
                principalTable: "Ride",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Driver_DriverId",
                table: "Vehicle",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Ride_RideId",
                table: "Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_Driver_DriverId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Ride_UserId",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Payment_RideId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Payment_UserId",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Driver_RideId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Driver");

            migrationBuilder.AlterColumn<Guid>(
                name: "DriverId",
                table: "Vehicle",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "RideId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_DriverId",
                table: "Ride",
                column: "DriverId",
                unique: true,
                filter: "[DriverId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_UserId",
                table: "Ride",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_RideId",
                table: "Payment",
                column: "RideId",
                unique: true,
                filter: "[RideId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_UserId",
                table: "Payment",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment",
                column: "RideId",
                principalTable: "Ride",
                principalColumn: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_User_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_DriverId",
                table: "Ride",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_Driver_DriverId",
                table: "Vehicle",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "DriverId");
        }
    }
}
