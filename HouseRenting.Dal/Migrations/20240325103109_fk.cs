using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class fk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "adverts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 3, 6, 11, 38, 15, 25, DateTimeKind.Local).AddTicks(1842));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "adverts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 6, 11, 38, 15, 25, DateTimeKind.Local).AddTicks(1842),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
