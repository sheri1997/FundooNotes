using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FundooRepositry.Migrations
{
    public partial class NotesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserModel",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Remainder",
                table: "Note",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Trash",
                table: "Note",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserModel",
                table: "User",
                column: "UserModel");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Note_UserModel",
                table: "User",
                column: "UserModel",
                principalTable: "Note",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Note_UserModel",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserModel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserModel",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Remainder",
                table: "Note");

            migrationBuilder.DropColumn(
                name: "Trash",
                table: "Note");
        }
    }
}
