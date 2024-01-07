using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.LoanDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public class LoansHandlers
{
    public static async Task<Ok<IEnumerable<LoanWithBookInfoDto>>> GetAllLoansForUserAsync (DataContext dataContext,
    IMapper mapper,
    int userId,
    [FromQuery] bool? onlyActive) 
    {
        var loans = await dataContext.Loans
            .Where(l=>l.UserId == userId)
            .Where(l=>onlyActive == null || onlyActive == false || l.ReturnDate == null)
            .Include(l=>l.Book)
            .ToListAsync();
        var mappedLoans =  mapper.Map<IEnumerable<LoanWithBookInfoDto>>(loans);
        return TypedResults.Ok(mappedLoans);
    }
    
    public static async Task<Results<NotFound, Ok<LoanDetailsDto>>> GetLoanByIdAsync (DataContext dataContext,
        IMapper mapper,
        int loanId)
    {
        // var loan = await dataContext.Loans.FindAsync(loanId);
        var loan = await dataContext.Loans
            .Include(l => l.Book)
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == loanId);

        if (loan == null)
        {
            return TypedResults.NotFound();
        }
        var mappedLoan = mapper.Map<LoanDetailsDto>(loan);
        return TypedResults.Ok(mappedLoan);
    }
    
   
    public static async Task<Results<NotFound, CreatedAtRoute<LoanDto>>> CreateLoanAsync(DataContext dataContext,
        IMapper mapper,
        CreateLoanDto createLoanDto)
    {
        var book = await dataContext.Books.FindAsync(createLoanDto.BookId);
        var user = await dataContext.Users.FindAsync(createLoanDto.UserId);
        if (book == null || user == null || book.CurrentStockLevel == 0)
        {
            return TypedResults.NotFound();
        }

        book.CurrentStockLevel -= 1;
        var loan = mapper.Map<Loan>(createLoanDto);
        dataContext.Add(loan);
        
        await dataContext.SaveChangesAsync();
    
        var loanToReturn = mapper.Map<LoanDto>(loan);
        return TypedResults.CreatedAtRoute(
            loanToReturn,
            "GetLoan",
            new {loanId=loanToReturn.Id}
        );
    }
    public static async Task<Results<NotFound, CreatedAtRoute<LoanDto>>> CreateLoanWithQueryParamsAsync(DataContext dataContext,
        IMapper mapper,
        [FromQuery] int userId,
        [FromQuery] int bookId)
    {
        var book = await dataContext.Books.FindAsync(bookId);
        var user = await dataContext.Users.FindAsync(userId);
        if (book == null || user == null || book.CurrentStockLevel == 0)
        {
            return TypedResults.NotFound();
        }

        book.CurrentStockLevel -= 1;
        var loan = new Loan { UserId = userId, BookId = bookId };
        dataContext.Add(loan);
        
        await dataContext.SaveChangesAsync();
    
        var loanToReturn = mapper.Map<LoanDto>(loan);
        return TypedResults.CreatedAtRoute(
            loanToReturn,
            "GetLoan",
            new {loanId=loanToReturn.Id}
        );
    }
    public static async Task<Results<NotFound, CreatedAtRoute<LoanDto>>> CloseALoanAsync(DataContext dataContext,
        IMapper mapper,
        [FromRoute] int loanId)
    {
        var loan = await dataContext.Loans.FindAsync(loanId);
        var book = await dataContext.Books.FindAsync(loan?.BookId);
        if (loan == null || book == null)
        {
            return TypedResults.NotFound();
        }
        
        book.CurrentStockLevel += 1;
        
        loan.ReturnDate = DateTime.Now;
        
        await dataContext.SaveChangesAsync();
    
        var loanToReturn = mapper.Map<LoanDto>(loan);
        return TypedResults.CreatedAtRoute(
            loanToReturn,
            "GetLoan",
            new {loanId=loanToReturn.Id}
        );
    }
   
    
    public static async Task<Results<NotFound, NoContent>> DeleteLoanAsync (DataContext dataContext, [FromRoute] int loanId) 
    { 
        var loan = await dataContext.Loans.FindAsync(loanId);
        // var loan = await dataContext.Loans.FirstOrDefaultAsync(a => a.Id == loanId);
        if (loan == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Loans.Remove(loan);
        dataContext.Remove(loan);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}