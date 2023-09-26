using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class ColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_RideId1",
                table: "Ride");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ride",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_RideId1",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "RideId1",
                table: "Ride");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "Vehicle",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Payment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Location",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Driver",
                newName: "Id");

            migrationBuilder.AlterColumn<Guid>(
                name: "RideId",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ride",
                table: "Ride",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_RideId",
                table: "Ride",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment",
                column: "RideId",
                principalTable: "Ride",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_RideId",
                table: "Ride",
                column: "RideId",
                principalTable: "Driver",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_RideId",
                table: "Ride");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ride",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_RideId",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ride");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vehicle",
                newName: "VehicleId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payment",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Location",
                newName: "LocationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Driver",
                newName: "DriverId");

            migrationBuilder.AlterColumn<Guid>(
                name: "RideId",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RideId1",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ride",
                table: "Ride",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_RideId1",
                table: "Ride",
                column: "RideId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Ride_RideId",
                table: "Payment",
                column: "RideId",
                principalTable: "Ride",
                principalColumn: "RideId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_RideId1",
                table: "Ride",
                column: "RideId1",
                principalTable: "Driver",
                principalColumn: "DriverId");
        }
    }
}
