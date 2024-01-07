using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovusLiberus.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueBookIdUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Reviews ADD CONSTRAINT UQ_Reviews_BookId_User_id UNIQUE(UserId, BookId)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Reviews DROP CONSTRAINT UQ_Reviews_BookId_User_id");
        }
    }
}
