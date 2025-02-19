using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    public partial class Roomsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove DropTable if Rooms table never existed
            migrationBuilder.CreateTable(
                name: "hostelRoom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    roomNo = table.Column<int>(type: "int", nullable: false),
                    floor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hostelRoom", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "hostelRoom");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    floor = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });
        }
    }
}
