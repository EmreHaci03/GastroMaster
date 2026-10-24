using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroMaster.DataAccessLayer.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableNumber",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "MenuTableId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_MenuTableId",
                table: "Orders",
                column: "MenuTableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_MenuTables_MenuTableId",
                table: "Orders",
                column: "MenuTableId",
                principalTable: "MenuTables",
                principalColumn: "MenuTableId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_MenuTables_MenuTableId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_MenuTableId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MenuTableId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "TableNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
