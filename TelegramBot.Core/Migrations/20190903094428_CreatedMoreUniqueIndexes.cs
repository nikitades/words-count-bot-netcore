using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBot.Core.Migrations
{
    public partial class CreatedMoreUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Words_Text",
                table: "Words",
                column: "Text",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_TelegramID",
                table: "Chats",
                column: "TelegramID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Words_Text",
                table: "Words");

            migrationBuilder.DropIndex(
                name: "IX_Chats_TelegramID",
                table: "Chats");
        }
    }
}
