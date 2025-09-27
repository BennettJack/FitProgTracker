using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fpt_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddedGloballyVisibletoexercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GloballyVisible",
                table: "Exercises",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GloballyVisible",
                table: "Exercises");
        }
    }
}
