using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class rateadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientID",
                table: "Opinion",
                newName: "rateOpinion");

            migrationBuilder.AddColumn<int>(
                name: "rateLocation",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rateLocation",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "rateOpinion",
                table: "Opinion",
                newName: "ClientID");
        }
    }
}
