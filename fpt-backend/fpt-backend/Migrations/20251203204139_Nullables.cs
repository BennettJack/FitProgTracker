using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class Nullables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseSessionId",
                table: "ExerciseSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "ExerciseSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseSessionId",
                table: "ExerciseSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "ExerciseSessionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
