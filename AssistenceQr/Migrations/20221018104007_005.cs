using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssistanceQr.Migrations
{
    public partial class _005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistances_Users_UserId",
                table: "Assistances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assistances",
                table: "Assistances");

            migrationBuilder.RenameTable(
                name: "Assistances",
                newName: "Assistance");

            migrationBuilder.RenameIndex(
                name: "IX_Assistances_UserId",
                table: "Assistance",
                newName: "IX_Assistance_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assistance",
                table: "Assistance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistance_Users_UserId",
                table: "Assistance",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistance_Users_UserId",
                table: "Assistance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assistance",
                table: "Assistance");

            migrationBuilder.RenameTable(
                name: "Assistance",
                newName: "Assistances");

            migrationBuilder.RenameIndex(
                name: "IX_Assistance_UserId",
                table: "Assistances",
                newName: "IX_Assistances_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assistances",
                table: "Assistances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistances_Users_UserId",
                table: "Assistances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
