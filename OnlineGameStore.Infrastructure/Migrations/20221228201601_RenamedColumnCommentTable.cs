using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineGameStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedColumnCommentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserIdCreated",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "UserIdCreated",
                table: "Comments",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserIdCreated",
                table: "Comments",
                newName: "IX_Comments_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_OwnerId",
                table: "Comments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_OwnerId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Comments",
                newName: "UserIdCreated");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                newName: "IX_Comments_UserIdCreated");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Comments",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserIdCreated",
                table: "Comments",
                column: "UserIdCreated",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
