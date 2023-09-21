using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Ride_RideId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_RideId",
                table: "Driver");

            migrationBuilder.AddColumn<Guid>(
                name: "RideId1",
                table: "Ride",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Ride",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ride_RideId1",
                table: "Ride",
                column: "RideId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ride_Driver_RideId1",
                table: "Ride",
                column: "RideId1",
                principalTable: "Driver",
                principalColumn: "DriverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ride_Driver_RideId1",
                table: "Ride");

            migrationBuilder.DropIndex(
                name: "IX_Ride_RideId1",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "RideId1",
                table: "Ride");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ride");

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
        }
    }
}
