using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GastroMaster.DataAccessLayer.Migrations
{
    public partial class mig13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Discounts");
        }
    }
}
