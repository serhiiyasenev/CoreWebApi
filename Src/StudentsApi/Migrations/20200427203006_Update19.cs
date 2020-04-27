using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApi.Migrations
{
    public partial class Update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Disciplines_Name",
                table: "Disciplines");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Disciplines",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Disciplines",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_Name",
                table: "Disciplines",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
