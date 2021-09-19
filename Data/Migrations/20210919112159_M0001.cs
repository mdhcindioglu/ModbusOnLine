using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Modbus.Data.Migrations
{
    public partial class M0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlcDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    D1 = table.Column<double>(type: "float", nullable: false),
                    D2 = table.Column<double>(type: "float", nullable: false),
                    D3 = table.Column<double>(type: "float", nullable: false),
                    D4 = table.Column<double>(type: "float", nullable: false),
                    D5 = table.Column<double>(type: "float", nullable: false),
                    D6 = table.Column<double>(type: "float", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlcDatas", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlcDatas");
        }
    }
}
