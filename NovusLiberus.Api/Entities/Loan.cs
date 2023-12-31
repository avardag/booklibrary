using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovusLiberus.Api.Entities;

public class Loan
{
    private readonly int _loanPeriod =14;
    public Loan()
    {
        LoanDate = DateTime.Now;
        DueDate = LoanDate.AddDays(_loanPeriod);
    }
    public int Id {get; set;}
    [Required]
    public DateTime LoanDate {get; set;}
    [Required]
    public DateTime DueDate {get; set;}
    public DateTime ReturnDate {get; set;}
  
    public int BookId {get; set;}
    public Book Book {get; set;}

    public int UserId {get; set;}
    public User User {get; set;}
}
//builder.Loan(x => x.LoanDate).HasDefaultValueSql("getdate()");