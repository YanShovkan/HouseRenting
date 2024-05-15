using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class AddedReviewedStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "adverts");

            migrationBuilder.AddColumn<int>(
                name: "ReviewStatus",
                table: "adverts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReviewStatus",
                table: "adverts");

            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "adverts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
