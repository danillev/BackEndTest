using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTest.Migrations
{
    /// <inheritdoc />
    public partial class Dbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cars",
                table: "Trains");

            migrationBuilder.CreateTable(
                name: "trainsCars",
                columns: table => new
                {
                    traintNumber = table.Column<int>(type: "integer", nullable: false),
                    carNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainsCars", x => new { x.traintNumber, x.carNumber });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trainsCars");

            migrationBuilder.AddColumn<List<int>>(
                name: "Cars",
                table: "Trains",
                type: "integer[]",
                nullable: false);
        }
    }
}
