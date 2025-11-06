using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedExerciseSetandrecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseExerciseSession");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseId",
                table: "ExerciseSessions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExerciseSet",
                columns: table => new
                {
                    ExerciseSetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSet", x => x.ExerciseSetId);
                    table.ForeignKey(
                        name: "FK_ExerciseSet_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSessionExerciseSet",
                columns: table => new
                {
                    ExerciseSessionId = table.Column<int>(type: "int", nullable: false),
                    ExerciseSetsExerciseSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessionExerciseSet", x => new { x.ExerciseSessionId, x.ExerciseSetsExerciseSetId });
                    table.ForeignKey(
                        name: "FK_ExerciseSessionExerciseSet_ExerciseSessions_ExerciseSessionId",
                        column: x => x.ExerciseSessionId,
                        principalTable: "ExerciseSessions",
                        principalColumn: "ExerciseSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSessionExerciseSet_ExerciseSet_ExerciseSetsExerciseSetId",
                        column: x => x.ExerciseSetsExerciseSetId,
                        principalTable: "ExerciseSet",
                        principalColumn: "ExerciseSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseSetRecord",
                columns: table => new
                {
                    ExerciseSetRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reps = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ExerciseSetId = table.Column<int>(type: "int", nullable: false),
                    ExerciseSessionId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSetRecord", x => x.ExerciseSetRecordId);
                    table.ForeignKey(
                        name: "FK_ExerciseSetRecord_ExerciseSessions_ExerciseSessionId",
                        column: x => x.ExerciseSessionId,
                        principalTable: "ExerciseSessions",
                        principalColumn: "ExerciseSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseSetRecord_ExerciseSet_ExerciseSetId",
                        column: x => x.ExerciseSetId,
                        principalTable: "ExerciseSet",
                        principalColumn: "ExerciseSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessions_ExerciseId",
                table: "ExerciseSessions",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionExerciseSet_ExerciseSetsExerciseSetId",
                table: "ExerciseSessionExerciseSet",
                column: "ExerciseSetsExerciseSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSet_ExerciseId",
                table: "ExerciseSet",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetRecord_ExerciseSessionId",
                table: "ExerciseSetRecord",
                column: "ExerciseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetRecord_ExerciseSetId",
                table: "ExerciseSetRecord",
                column: "ExerciseSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSessions_Exercises_ExerciseId",
                table: "ExerciseSessions",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSessions_Exercises_ExerciseId",
                table: "ExerciseSessions");

            migrationBuilder.DropTable(
                name: "ExerciseSessionExerciseSet");

            migrationBuilder.DropTable(
                name: "ExerciseSetRecord");

            migrationBuilder.DropTable(
                name: "ExerciseSet");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSessions_ExerciseId",
                table: "ExerciseSessions");

            migrationBuilder.DropColumn(
                name: "ExerciseId",
                table: "ExerciseSessions");

            migrationBuilder.CreateTable(
                name: "ExerciseExerciseSession",
                columns: table => new
                {
                    ExerciseSessionsExerciseSessionId = table.Column<int>(type: "int", nullable: false),
                    ExercisesExerciseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseExerciseSession", x => new { x.ExerciseSessionsExerciseSessionId, x.ExercisesExerciseId });
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseSession_ExerciseSessions_ExerciseSessionsExerciseSessionId",
                        column: x => x.ExerciseSessionsExerciseSessionId,
                        principalTable: "ExerciseSessions",
                        principalColumn: "ExerciseSessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseExerciseSession_Exercises_ExercisesExerciseId",
                        column: x => x.ExercisesExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "ExerciseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseExerciseSession_ExercisesExerciseId",
                table: "ExerciseExerciseSession",
                column: "ExercisesExerciseId");
        }
    }
}
