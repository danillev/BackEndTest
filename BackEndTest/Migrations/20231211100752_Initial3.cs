using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEndTest.Migrations
{
    /// <inheritdoc />
    public partial class Initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "dateAndTimeLastOperation",
                table: "Trains",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "fromStationName",
                table: "Trains",
                type: "text",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<string>(
                name: "toStationName",
                table: "Trains",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trainIndexCombined",
                table: "Trains",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    carNumber = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invoiceNumber = table.Column<string>(type: "text", nullable: false),
                    positionInTrain = table.Column<int>(type: "integer", nullable: false),
                    freightEtsngName = table.Column<string>(type: "text", nullable: false),
                    freightTotalWeightKg = table.Column<int>(type: "integer", nullable: false),
                    trainNumber = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.carNumber);
                    table.ForeignKey(
                        name: "FK_Car_Trains_trainNumber",
                        column: x => x.trainNumber,
                        principalTable: "Trains",
                        principalColumn: "trainNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Car_trainNumber",
                table: "Car",
                column: "trainNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropColumn(
                name: "dateAndTimeLastOperation",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "fromStationName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "lastOperationName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "lastStationName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "toStationName",
                table: "Trains");

            migrationBuilder.DropColumn(
                name: "trainIndexCombined",
                table: "Trains");
        }
    }
}
