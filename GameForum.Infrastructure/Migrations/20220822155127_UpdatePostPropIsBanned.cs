using Microsoft.EntityFrameworkCore.Migrations;

namespace GameForum.Infrastructure.Migrations
{
    public partial class UpdatePostPropIsBanned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBanned",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBanned",
                table: "Posts");
        }
    }
}
