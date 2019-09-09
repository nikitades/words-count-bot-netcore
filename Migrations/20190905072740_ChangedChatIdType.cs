using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsCountBot.Migrations
{
    public partial class ChangedChatIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TelegramID",
                table: "Chats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TelegramID",
                table: "Chats",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
