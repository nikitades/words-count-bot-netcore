using Microsoft.EntityFrameworkCore.Migrations;

namespace WordsCountBot.Migrations
{
    public partial class RenamedWordUsedTimesUsages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usage_Chats_ChatID",
                table: "Usage");

            migrationBuilder.DropForeignKey(
                name: "FK_Usage_Words_WordID",
                table: "Usage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usage",
                table: "Usage");

            migrationBuilder.RenameTable(
                name: "Usage",
                newName: "Usages");

            migrationBuilder.RenameIndex(
                name: "IX_Usage_WordID",
                table: "Usages",
                newName: "IX_Usages_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Usage_ChatID",
                table: "Usages",
                newName: "IX_Usages_ChatID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usages",
                table: "Usages",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usages_Chats_ChatID",
                table: "Usages",
                column: "ChatID",
                principalTable: "Chats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usages_Words_WordID",
                table: "Usages",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usages_Chats_ChatID",
                table: "Usages");

            migrationBuilder.DropForeignKey(
                name: "FK_Usages_Words_WordID",
                table: "Usages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usages",
                table: "Usages");

            migrationBuilder.RenameTable(
                name: "Usages",
                newName: "Usage");

            migrationBuilder.RenameIndex(
                name: "IX_Usages_WordID",
                table: "Usage",
                newName: "IX_Usage_WordID");

            migrationBuilder.RenameIndex(
                name: "IX_Usages_ChatID",
                table: "Usage",
                newName: "IX_Usage_ChatID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usage",
                table: "Usage",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_Chats_ChatID",
                table: "Usage",
                column: "ChatID",
                principalTable: "Chats",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usage_Words_WordID",
                table: "Usage",
                column: "WordID",
                principalTable: "Words",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
