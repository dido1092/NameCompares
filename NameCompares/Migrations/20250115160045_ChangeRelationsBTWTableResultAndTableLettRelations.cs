using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NameCompares.Migrations
{
    public partial class ChangeRelationsBTWTableResultAndTableLettRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LettRelations_ResultTables_ResultTableId",
                table: "LettRelations");

            migrationBuilder.DropIndex(
                name: "IX_LettRelations_ResultTableId",
                table: "LettRelations");

            migrationBuilder.DropColumn(
                name: "ResultTableId",
                table: "LettRelations");

            migrationBuilder.AddColumn<int>(
                name: "LettRelationsId",
                table: "ResultTables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResultTables_LettRelationsId",
                table: "ResultTables",
                column: "LettRelationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResultTables_LettRelations_LettRelationsId",
                table: "ResultTables",
                column: "LettRelationsId",
                principalTable: "LettRelations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResultTables_LettRelations_LettRelationsId",
                table: "ResultTables");

            migrationBuilder.DropIndex(
                name: "IX_ResultTables_LettRelationsId",
                table: "ResultTables");

            migrationBuilder.DropColumn(
                name: "LettRelationsId",
                table: "ResultTables");

            migrationBuilder.AddColumn<int>(
                name: "ResultTableId",
                table: "LettRelations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LettRelations_ResultTableId",
                table: "LettRelations",
                column: "ResultTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_LettRelations_ResultTables_ResultTableId",
                table: "LettRelations",
                column: "ResultTableId",
                principalTable: "ResultTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
