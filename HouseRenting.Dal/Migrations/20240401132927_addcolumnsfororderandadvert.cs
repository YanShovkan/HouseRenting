using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class addcolumnsfororderandadvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sum",
                table: "orders",
                newName: "TotalSum");

            migrationBuilder.AddColumn<decimal>(
                name: "AgencyPercent",
                table: "orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AgencySum",
                table: "orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MounthCount",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MounthPrice",
                table: "orders",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ClientEmail",
                table: "adverts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClientFIO",
                table: "adverts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgencyPercent",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "AgencySum",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "MounthCount",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "MounthPrice",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ClientEmail",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "ClientFIO",
                table: "adverts");

            migrationBuilder.RenameColumn(
                name: "TotalSum",
                table: "orders",
                newName: "Sum");
        }
    }
}
