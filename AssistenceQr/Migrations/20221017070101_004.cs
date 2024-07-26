using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssistanceQr.Migrations
{
    public partial class _004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaternarSurname",
                table: "Users",
                newName: "MaternalSurname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaternalSurname",
                table: "Users",
                newName: "MaternarSurname");
        }
    }
}
