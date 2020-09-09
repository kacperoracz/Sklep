using Microsoft.EntityFrameworkCore.Migrations;

namespace Sklep.Migrations
{
    public partial class AddDescriptionToRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rates");
        }
    }
}
