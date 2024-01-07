using System.ComponentModel.DataAnnotations;

namespace NovusLiberus.Api.Entities;

public class Review
{
    public int Id { get; set; }
    [MaxLength(248, ErrorMessage = "Max 248 chars for comment")]
    public string? Comment { get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
    public int Rating { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}