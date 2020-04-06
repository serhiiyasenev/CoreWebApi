using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FirstWebApplication.Migrations
{
    public partial class TeachersStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_StudentEntity_StudentName",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_TeacherEntity_TeacherName",
                table: "StudentTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTeacher",
                table: "StudentTeacher");

            migrationBuilder.DropIndex(
                name: "IX_StudentTeacher_TeacherName",
                table: "StudentTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "StudentTeacher");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "StudentTeacher");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TeacherEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "TeacherEntity",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentTeacher",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "StudentTeacher",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentEntity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentEntity",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity",
                column: "TeacherId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTeacher",
                table: "StudentTeacher",
                columns: new[] { "StudentId", "TeacherId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacher_TeacherId",
                table: "StudentTeacher",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_StudentEntity_StudentId",
                table: "StudentTeacher",
                column: "StudentId",
                principalTable: "StudentEntity",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_TeacherEntity_TeacherId",
                table: "StudentTeacher",
                column: "TeacherId",
                principalTable: "TeacherEntity",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_StudentEntity_StudentId",
                table: "StudentTeacher");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeacher_TeacherEntity_TeacherId",
                table: "StudentTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentTeacher",
                table: "StudentTeacher");

            migrationBuilder.DropIndex(
                name: "IX_StudentTeacher_TeacherId",
                table: "StudentTeacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "TeacherEntity");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentTeacher");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "StudentTeacher");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "TeacherEntity",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "StudentTeacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "StudentTeacher",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "StudentEntity",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeacherEntity",
                table: "TeacherEntity",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentTeacher",
                table: "StudentTeacher",
                columns: new[] { "StudentName", "TeacherName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentEntity",
                table: "StudentEntity",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacher_TeacherName",
                table: "StudentTeacher",
                column: "TeacherName");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_StudentEntity_StudentName",
                table: "StudentTeacher",
                column: "StudentName",
                principalTable: "StudentEntity",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeacher_TeacherEntity_TeacherName",
                table: "StudentTeacher",
                column: "TeacherName",
                principalTable: "TeacherEntity",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
