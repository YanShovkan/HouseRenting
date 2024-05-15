using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class AddMonthCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthCount",
                table: "requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthCount",
                table: "requests");
        }
    }
}
