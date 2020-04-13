using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsApi.Migrations
{
    public partial class UpdateStudents1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentEntity",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(nullable: true),
                    AvrScore = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEntity", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "TeacherEntity",
                columns: table => new
                {
                    TeacherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherName = table.Column<string>(nullable: true),
                    Discipline = table.Column<string>(nullable: true),
                    GroupEntityGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherEntity", x => x.TeacherId);
                });

            migrationBuilder.CreateTable(
                name: "GroupEntity",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Course = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: true),
                    TeacherEntityTeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupEntity", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_GroupEntity_TeacherEntity_TeacherEntityTeacherId",
                        column: x => x.TeacherEntityTeacherId,
                        principalTable: "TeacherEntity",
                        principalColumn: "TeacherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupEntity_TeacherEntityTeacherId",
                table: "GroupEntity",
                column: "TeacherEntityTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentEntity_GroupId",
                table: "StudentEntity",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherEntity_GroupEntityGroupId",
                table: "TeacherEntity",
                column: "GroupEntityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEntity_GroupEntity_GroupId",
                table: "StudentEntity",
                column: "GroupId",
                principalTable: "GroupEntity",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherEntity_GroupEntity_GroupEntityGroupId",
                table: "TeacherEntity",
                column: "GroupEntityGroupId",
                principalTable: "GroupEntity",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupEntity_TeacherEntity_TeacherEntityTeacherId",
                table: "GroupEntity");

            migrationBuilder.DropTable(
                name: "StudentEntity");

            migrationBuilder.DropTable(
                name: "TeacherEntity");

            migrationBuilder.DropTable(
                name: "GroupEntity");
        }
    }
}
