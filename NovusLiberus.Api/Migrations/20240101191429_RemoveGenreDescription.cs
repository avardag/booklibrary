using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovusLiberus.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGenreDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "nvarchar(124)",
                maxLength: 124,
                nullable: true);
        }
    }
}
