using Microsoft.EntityFrameworkCore.Migrations;

namespace GameForum.Infrastructure.Migrations
{
    public partial class RemoveParagraphOrderProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Paragraphs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Paragraphs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
