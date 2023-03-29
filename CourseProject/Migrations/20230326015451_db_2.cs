using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class db_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "InternalRating",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalRating",
                table: "Books");

            migrationBuilder.AddColumn<float>(
                name: "Rating",
                table: "Books",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
