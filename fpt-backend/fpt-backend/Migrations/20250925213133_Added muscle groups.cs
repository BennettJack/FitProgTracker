using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class Addedmusclegroups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MuscleGroupId",
                table: "Muscles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MuscleGroups",
                columns: table => new
                {
                    MuscleGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MuscleGroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleGroups", x => x.MuscleGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Muscles_MuscleGroupId",
                table: "Muscles",
                column: "MuscleGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "MuscleGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscles_MuscleGroups_MuscleGroupId",
                table: "Muscles");

            migrationBuilder.DropTable(
                name: "MuscleGroups");

            migrationBuilder.DropIndex(
                name: "IX_Muscles_MuscleGroupId",
                table: "Muscles");

            migrationBuilder.DropColumn(
                name: "MuscleGroupId",
                table: "Muscles");
        }
    }
}
