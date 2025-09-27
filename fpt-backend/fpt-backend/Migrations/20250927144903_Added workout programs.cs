using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class Addedworkoutprograms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkoutProgramId",
                table: "ExerciseSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutPrograms",
                columns: table => new
                {
                    WorkoutProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutProgramName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkoutProgramDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAccessToProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPrograms", x => x.WorkoutProgramId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessions_WorkoutProgramId",
                table: "ExerciseSessions",
                column: "WorkoutProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSessions_WorkoutPrograms_WorkoutProgramId",
                table: "ExerciseSessions",
                column: "WorkoutProgramId",
                principalTable: "WorkoutPrograms",
                principalColumn: "WorkoutProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSessions_WorkoutPrograms_WorkoutProgramId",
                table: "ExerciseSessions");

            migrationBuilder.DropTable(
                name: "WorkoutPrograms");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSessions_WorkoutProgramId",
                table: "ExerciseSessions");

            migrationBuilder.DropColumn(
                name: "WorkoutProgramId",
                table: "ExerciseSessions");
        }
    }
}
