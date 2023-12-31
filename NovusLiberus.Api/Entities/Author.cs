using System.ComponentModel.DataAnnotations;

namespace NovusLiberus.Api.Entities;

public class Author 
{
    public int Id { get; set; }
    [Required]
    [StringLength(64, ErrorMessage = "First Name must be from 0 to 64 characters long")]
    public string FirstName { get; set; }
    [StringLength(50, ErrorMessage = "Middle Name must be from 0 to 50 characters long")]
    public string? MiddleName { get; set; }
    [Required]
    [StringLength(64, ErrorMessage = "Last Name must be from 0 to 64 characters long")]
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfDeath { get; set; }
    
    public List<Book> Books { get; set; }
}