using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NameCompares.Migrations
{
    public partial class AddNewTableWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BgWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FWordLength = table.Column<int>(type: "int", nullable: false),
                    BgWordLength = table.Column<int>(type: "int", nullable: false),
                    Comparison = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LettRelationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordTables_LettRelations_LettRelationsId",
                        column: x => x.LettRelationsId,
                        principalTable: "LettRelations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordTables_LettRelationsId",
                table: "WordTables",
                column: "LettRelationsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordTables");
        }
    }
}
