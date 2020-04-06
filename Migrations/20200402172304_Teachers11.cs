using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FirstWebApplication.Migrations
{
    public partial class Teachers11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Teachers_TeacherName",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_TeacherName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "Students");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "StudentEntity",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntity", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEntity",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Discipline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEntity", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "StudentTeacher",
                columns: table => new
                {
                    StudentName = table.Column<string>(nullable: false),
                    TeacherName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacher", x => new { x.StudentName, x.TeacherName });
                    table.ForeignKey(
                        name: "FK_StudentTeacher_StudentEntity_StudentName",
                        column: x => x.StudentName,
                        principalTable: "StudentEntity",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTeacher_TeacherEntity_TeacherName",
                        column: x => x.TeacherName,
                        principalTable: "TeacherEntity",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StudentName",
                table: "Teachers",
                column: "StudentName");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacher_TeacherName",
                table: "StudentTeacher",
                column: "TeacherName");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Students_StudentName",
                table: "Teachers",
                column: "StudentName",
                principalTable: "Students",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Students_StudentName",
                table: "Teachers");

            migrationBuilder.DropTable(
                name: "StudentTeacher");

            migrationBuilder.DropTable(
                name: "StudentEntity");

            migrationBuilder.DropTable(
                name: "TeacherEntity");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StudentName",
                table: "Teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Students",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Teachers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Students",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Students",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "Students",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Students",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_TeacherName",
                table: "Students",
                column: "TeacherName");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Teachers_TeacherName",
                table: "Students",
                column: "TeacherName",
                principalTable: "Teachers",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
