using System.ComponentModel.DataAnnotations;

namespace NovusLiberus.Api.Entities;

public class Book 
{
    public int Id { get; set; }
    [Required]
    [StringLength(13, ErrorMessage = "Genre name must be between 9 to 13 characters long")]
    public string ISBN { get; set; }
    [Required]
    [StringLength(248, ErrorMessage = "Genre name must be between 1 to 248 characters long")]
    public string Title { get; set; }
    public DateTime PubDate { get; set; }
    public double AvgRating { get; set; }
    [Required]
    [Range(0, 100)]
    public int CurrentStockLevel { get; set; }
    
    public List<Author> Authors { get; set; }
    public List<Genre> Genres { get; set; }
    
    public List<Loan> Loans {get; set;}
    public List<Review> Reviews {get; set;}
}

// modelBuilder.Entity<Book>()
//     .HasMany(b => b.Genres)
//     .WithMany(g => g.Books)
//     .UsingEntity<Dictionary<string, object>>(
//         "BookGenre", 
//         bg => {
//             bg.HasOne<Genre>().WithMany().HasForeignKey("GenreId");
//             bg.HasOne<Book>().WithMany().HasForeignKey("BookId");
//         }
//     );