using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class commentAndStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "requests");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "adverts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "requests");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "adverts");

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "requests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
