using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Acme.BookStore.Migrations
{
    public partial class add_isbn_OwnedType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppBooks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CheckDigit",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EAN",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "AppBooks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AppBooks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "CheckDigit",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "EAN",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "AppBooks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppBooks");
        }
    }
}
