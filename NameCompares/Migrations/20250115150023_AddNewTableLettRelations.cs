using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NameCompares.Migrations
{
    public partial class AddNewTableLettRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "BgNames",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BgName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BgNames", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "BgWords",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        BgWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_BgWords", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EnBgMatchesWords",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EnWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BgWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EnBgMatchesWords", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EnNames",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        EnName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EnNames", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "LatWords",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LatWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        EnWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LatWords", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "PtNames",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        PtName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PtNames", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ResultTables",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        LatWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        BgWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        LatWordLength = table.Column<int>(type: "int", nullable: false),
            //        BgWordLength = table.Column<int>(type: "int", nullable: false),
            //        Comparison = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ResultTables", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WorldNames",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        WorldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Length = table.Column<int>(type: "int", nullable: false),
            //        DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WorldNames", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "LettRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Letters = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResultTableId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LettRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LettRelations_ResultTables_ResultTableId",
                        column: x => x.ResultTableId,
                        principalTable: "ResultTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LettRelations_ResultTableId",
                table: "LettRelations",
                column: "ResultTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BgNames");

            migrationBuilder.DropTable(
                name: "BgWords");

            migrationBuilder.DropTable(
                name: "EnBgMatchesWords");

            migrationBuilder.DropTable(
                name: "EnNames");

            migrationBuilder.DropTable(
                name: "LatWords");

            migrationBuilder.DropTable(
                name: "LettRelations");

            migrationBuilder.DropTable(
                name: "PtNames");

            migrationBuilder.DropTable(
                name: "WorldNames");

            migrationBuilder.DropTable(
                name: "ResultTables");
        }
    }
}
