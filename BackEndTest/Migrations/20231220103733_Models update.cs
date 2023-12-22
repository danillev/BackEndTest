using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTest.Migrations
{
    /// <inheritdoc />
    public partial class Modelsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateAndTimeLastOperation",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "lastOperationName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "lastStationName",
                table: "Trains");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateAndTimeLastOperation",
                table: "cars",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastOperationName",
                table: "cars",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastStationName",
                table: "cars",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateAndTimeLastOperation",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "lastOperationName",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "lastStationName",
                table: "cars");

            migrationBuilder.AddColumn<DateTime>(
                name: "dateAndTimeLastOperation",
                table: "Trains",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "lastOperationName",
                table: "Trains",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "lastStationName",
                table: "Trains",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
