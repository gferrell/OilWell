using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OilWellToolService.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedToolType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OilWellTool",
                columns: table => new
                {
                    AssetId = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Length = table.Column<double>(type: "REAL", nullable: false),
                    Diameter = table.Column<double>(type: "REAL", nullable: false),
                    ServiceDateDue = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OilWellTool", x => x.AssetId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OilWellTool");
        }
    }
}
