using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRenting.Dal.Migrations
{
    public partial class AddUserAdvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "adverts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_adverts_UserId",
                table: "adverts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_adverts_users_UserId",
                table: "adverts",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adverts_users_UserId",
                table: "adverts");

            migrationBuilder.DropIndex(
                name: "IX_adverts_UserId",
                table: "adverts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "adverts");
        }
    }
}
