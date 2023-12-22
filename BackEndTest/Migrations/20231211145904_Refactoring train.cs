using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTest.Migrations
{
    /// <inheritdoc />
    public partial class Refactoringtrain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Trains_trainNumber",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_trainNumber",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "trainNumber",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "cars");

            migrationBuilder.AddColumn<List<int>>(
                name: "Cars",
                table: "Trains",
                type: "integer[]",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                column: "carNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.DropColumn(
                name: "Cars",
                table: "Trains");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "Car");

            migrationBuilder.AddColumn<int>(
                name: "trainNumber",
                table: "Car",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "carNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Car_trainNumber",
                table: "Car",
                column: "trainNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Trains_trainNumber",
                table: "Car",
                column: "trainNumber",
                principalTable: "Trains",
                principalColumn: "trainNumber");
        }
    }
}
