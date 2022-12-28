using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGameStore.Infrastructure.Migrations
{
    public partial class RenamedColumnUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "Users",
                newName: "AvatarUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Users",
                newName: "Avatar");
        }
    }
}
