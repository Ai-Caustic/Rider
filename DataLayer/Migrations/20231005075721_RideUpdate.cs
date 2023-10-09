using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class RideUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_RideId",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_RideId",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "DestinationLatitude",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "DestinationLongitude",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "PickUpLatitude",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "PickUpLongitude",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Ride");

            migrationBuilder.AlterColumn<string>(
                name: "PickupLocation",
                table: "Ride",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Ride",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ride_DriverId",
                table: "Ride",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_DriverId",
                table: "Ride",
                column: "DriverId",
                principalTable: "Driver",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_DriverId",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_DriverId",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Ride");

            migrationBuilder.AlterColumn<string>(
                name: "PickupLocation",
                table: "Ride",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Ride",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "DestinationLatitude",
                table: "Ride",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DestinationLongitude",
                table: "Ride",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PickUpLatitude",
                table: "Ride",
                type: "decimal(8,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PickUpLongitude",
                table: "Ride",
                type: "decimal(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "RideId",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ride_RideId",
                table: "Ride",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_RideId",
                table: "Ride",
                column: "RideId",
                principalTable: "Driver",
                principalColumn: "Id");
        }
    }
}
