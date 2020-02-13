using Microsoft.EntityFrameworkCore.Migrations;

namespace TelegramBot.Core.Migrations
{
    public partial class CreatedUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usages_WordID",
                table: "Usages");

            migrationBuilder.CreateIndex(
                name: "IX_Usages_WordID_ChatID",
                table: "Usages",
                columns: new[] { "WordID", "ChatID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usages_WordID_ChatID",
                table: "Usages");

            migrationBuilder.CreateIndex(
                name: "IX_Usages_WordID",
                table: "Usages",
                column: "WordID");
        }
    }
}
