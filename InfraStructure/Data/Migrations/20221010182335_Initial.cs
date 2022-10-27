using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InfraStructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dailies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dailies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Boxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollageId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boxes_Collages_CollageId",
                        column: x => x.CollageId,
                        principalTable: "Collages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoxId = table.Column<int>(type: "int", nullable: false),
                    DailyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyBoxes_Boxes_BoxId",
                        column: x => x.BoxId,
                        principalTable: "Boxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyBoxes_Dailies_DailyId",
                        column: x => x.DailyId,
                        principalTable: "Dailies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Num224 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DailyBoxId = table.Column<int>(type: "int", nullable: false),
                    TaxNormal = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    Stamp = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    Taxsettlement = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    Tax2 = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    Other = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    SumTax = table.Column<decimal>(type: "decimal(8,2)", nullable: false, computedColumnSql: "[TaxNormal]+[Stamp]+[Taxsettlement]+[Tax2]+[Other]"),
                    TaxDevelopment = table.Column<decimal>(type: "decimal(8,2)", nullable: false, defaultValue: 0m),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_DailyBoxes_DailyBoxId",
                        column: x => x.DailyBoxId,
                        principalTable: "DailyBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_CollageId",
                table: "Boxes",
                column: "CollageId");

            migrationBuilder.CreateIndex(
                name: "IX_Dailies_DailyDate",
                table: "Dailies",
                column: "DailyDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyBoxes_BoxId_DailyId",
                table: "DailyBoxes",
                columns: new[] { "BoxId", "DailyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DailyBoxes_DailyId",
                table: "DailyBoxes",
                column: "DailyId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_DailyBoxId",
                table: "Forms",
                column: "DailyBoxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "DailyBoxes");

            migrationBuilder.DropTable(
                name: "Boxes");

            migrationBuilder.DropTable(
                name: "Dailies");

            migrationBuilder.DropTable(
                name: "Collages");
        }
    }
}
