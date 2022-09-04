using Microsoft.EntityFrameworkCore.Migrations;

namespace GameForum.Infrastructure.Migrations
{
    public partial class UpdatePostProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReported",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "IsReported",
                table: "Posts");
        }
    }
}
