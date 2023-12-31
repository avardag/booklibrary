using System.ComponentModel.DataAnnotations;

namespace NovusLiberus.Api.Entities;

public class Genre
{
    public int Id { get; set; }
    [Required]
    [StringLength(64, ErrorMessage = "Genre name must be between 0 to 64 characters long")]
    public string Name { get; set; }
    [MaxLength(124, ErrorMessage = "Max 124 chars for comment")]
    public string? Description { get; set; }
    
    public List<Book> Books { get; set; }
}