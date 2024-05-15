using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class AddRequesst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    PriceLess = table.Column<decimal>(type: "numeric", nullable: true),
                    PriceGreater = table.Column<decimal>(type: "numeric", nullable: true),
                    AreaLess = table.Column<decimal>(type: "numeric", nullable: true),
                    AreaGreater = table.Column<decimal>(type: "numeric", nullable: true),
                    RoomsCountLess = table.Column<int>(type: "integer", nullable: true),
                    RoomsCountGreater = table.Column<int>(type: "integer", nullable: true),
                    IsFinished = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requests_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requests_UserId",
                table: "requests",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requests");
        }
    }
}
