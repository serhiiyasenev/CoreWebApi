using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace StudentsApi.Migrations
{
    public partial class UpdateStudents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Discipline = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentTeacherEntity",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeacherEntity", x => new { x.StudentId, x.TeacherId });
                    table.ForeignKey(
                        name: "FK_StudentTeacherEntity_StudentEntity_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTeacherEntity_TeacherEntity_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "TeacherEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeacherEntity_TeacherId",
                table: "StudentTeacherEntity",
                column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentTeacherEntity");

            migrationBuilder.DropTable(
                name: "StudentEntity");

            migrationBuilder.DropTable(
                name: "TeacherEntity");
        }
    }
}
