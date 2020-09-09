using Microsoft.EntityFrameworkCore.Migrations;

namespace Sklep.Migrations
{
    public partial class AddUserIdToRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rates");
        }
    }
}
