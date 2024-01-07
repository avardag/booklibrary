using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovusLiberus.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerBookRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER TRIGGER UpdateBookRating
                ON Reviews
                AFTER INSERT, UPDATE, DELETE 
                AS
                BEGIN
                  UPDATE b
                  SET b.AvgRating = (SELECT AVG(r.Rating)
                                         FROM Reviews r
                                         WHERE r.BookId = b.Id)
                  FROM Books b
                  INNER JOIN inserted i ON b.Id = i.BookId;
                  
                  UPDATE b
                  SET b.AvgRating = (SELECT AVG(r.Rating)
                                         FROM Reviews r
                                         WHERE r.BookId = b.Id)
                  FROM Books b
                  INNER JOIN deleted d ON b.Id = d.BookId;
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER UpdateBookRating");
        }
    }
}
