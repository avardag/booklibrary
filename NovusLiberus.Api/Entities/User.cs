using System.ComponentModel.DataAnnotations;

namespace NovusLiberus.Api.Entities;

public class User 
{
    public int Id { get; set; }
    [Required]
    [StringLength(64)]
    public string FirstName { get; set; } 
    [StringLength(50)]
    public string? MiddleName { get; set; }
    [Required]
    [StringLength(64)]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    [Required]
    public Guid LibraryCardNumber { get; set; }
    
    public List<Review> Reviews {get; set;}
    public List<Loan> Loans {get; set;}
}