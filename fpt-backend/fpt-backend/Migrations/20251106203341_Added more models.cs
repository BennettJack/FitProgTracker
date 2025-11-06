using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class Addedmoremodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSet_Exercises_ExerciseId",
                table: "ExerciseSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSetRecord_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSetRecord");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSetRecord_ExerciseSet_ExerciseSetId",
                table: "ExerciseSetRecord");

            migrationBuilder.DropTable(
                name: "ExerciseSessionExerciseSet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseSetRecord",
                table: "ExerciseSetRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseSet",
                table: "ExerciseSet");

            migrationBuilder.RenameTable(
                name: "ExerciseSetRecord",
                newName: "ExerciseSetRecords");

            migrationBuilder.RenameTable(
                name: "ExerciseSet",
                newName: "ExerciseSets");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSetRecord_ExerciseSetId",
                table: "ExerciseSetRecords",
                newName: "IX_ExerciseSetRecords_ExerciseSetId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSetRecord_ExerciseSessionId",
                table: "ExerciseSetRecords",
                newName: "IX_ExerciseSetRecords_ExerciseSessionId");

            migrationBuilder.RenameColumn(
                name: "Reps",
                table: "ExerciseSets",
                newName: "RepFloor");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSet_ExerciseId",
                table: "ExerciseSets",
                newName: "IX_ExerciseSets_ExerciseId");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseSessionId",
                table: "ExerciseSetRecords",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ExerciseSessionRecordId",
                table: "ExerciseSetRecords",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExerciseSessionId",
                table: "ExerciseSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepCeiling",
                table: "ExerciseSets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseSetRecords",
                table: "ExerciseSetRecords",
                column: "ExerciseSetRecordId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseSets",
                table: "ExerciseSets",
                column: "ExerciseSetId");

            migrationBuilder.CreateTable(
                name: "ExerciseSessionRecords",
                columns: table => new
                {
                    ExerciseSessionRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseSetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseSessionRecords", x => x.ExerciseSessionRecordId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSetRecords_ExerciseSessionRecordId",
                table: "ExerciseSetRecords",
                column: "ExerciseSessionRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSets_ExerciseSessionId",
                table: "ExerciseSets",
                column: "ExerciseSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSessionRecords_ExerciseSessionRecordId",
                table: "ExerciseSetRecords",
                column: "ExerciseSessionRecordId",
                principalTable: "ExerciseSessionRecords",
                principalColumn: "ExerciseSessionRecordId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSetRecords",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "ExerciseSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSets_ExerciseSetId",
                table: "ExerciseSetRecords",
                column: "ExerciseSetId",
                principalTable: "ExerciseSets",
                principalColumn: "ExerciseSetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "ExerciseSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSets_Exercises_ExerciseId",
                table: "ExerciseSets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSessionRecords_ExerciseSessionRecordId",
                table: "ExerciseSetRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSetRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSetRecords_ExerciseSets_ExerciseSetId",
                table: "ExerciseSetRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSets");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseSets_Exercises_ExerciseId",
                table: "ExerciseSets");

            migrationBuilder.DropTable(
                name: "ExerciseSessionRecords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseSets",
                table: "ExerciseSets");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSets_ExerciseSessionId",
                table: "ExerciseSets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseSetRecords",
                table: "ExerciseSetRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseSetRecords_ExerciseSessionRecordId",
                table: "ExerciseSetRecords");

            migrationBuilder.DropColumn(
                name: "ExerciseSessionId",
                table: "ExerciseSets");

            migrationBuilder.DropColumn(
                name: "RepCeiling",
                table: "ExerciseSets");

            migrationBuilder.DropColumn(
                name: "ExerciseSessionRecordId",
                table: "ExerciseSetRecords");

            migrationBuilder.RenameTable(
                name: "ExerciseSets",
                newName: "ExerciseSet");

            migrationBuilder.RenameTable(
                name: "ExerciseSetRecords",
                newName: "ExerciseSetRecord");

            migrationBuilder.RenameColumn(
                name: "RepFloor",
                table: "ExerciseSet",
                newName: "Reps");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSets_ExerciseId",
                table: "ExerciseSet",
                newName: "IX_ExerciseSet_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSetRecords_ExerciseSetId",
                table: "ExerciseSetRecord",
                newName: "IX_ExerciseSetRecord_ExerciseSetId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseSetRecords_ExerciseSessionId",
                table: "ExerciseSetRecord",
                newName: "IX_ExerciseSetRecord_ExerciseSessionId");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseSessionId",
                table: "ExerciseSetRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseSet",
                table: "ExerciseSet",
                column: "ExerciseSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseSetRecord",
                table: "ExerciseSetRecord",
                column: "ExerciseSetRecordId");

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

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseSessionExerciseSet_ExerciseSetsExerciseSetId",
                table: "ExerciseSessionExerciseSet",
                column: "ExerciseSetsExerciseSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSet_Exercises_ExerciseId",
                table: "ExerciseSet",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "ExerciseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSetRecord_ExerciseSessions_ExerciseSessionId",
                table: "ExerciseSetRecord",
                column: "ExerciseSessionId",
                principalTable: "ExerciseSessions",
                principalColumn: "ExerciseSessionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseSetRecord_ExerciseSet_ExerciseSetId",
                table: "ExerciseSetRecord",
                column: "ExerciseSetId",
                principalTable: "ExerciseSet",
                principalColumn: "ExerciseSetId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
