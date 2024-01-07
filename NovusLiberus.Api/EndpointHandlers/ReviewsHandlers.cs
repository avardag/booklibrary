using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.ReviewDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.EndpointHandlers;

public class ReviewsHandlers
{
    public static async Task<Ok<IEnumerable<ReviewDto>>> GetAllReviewsForBookAsync (DataContext dataContext,
    IMapper mapper,
    int bookId) 
    {
        var reviews = await dataContext.Reviews.Where(r=>r.BookId == bookId).ToListAsync();
        var mappedReviews =  mapper.Map<IEnumerable<ReviewDto>>(reviews);
        return TypedResults.Ok(mappedReviews);
    }
    
    public static async Task<Results<NotFound, Ok<ReviewDto>>> GetReviewByIdAsync (DataContext dataContext,
        IMapper mapper,
        int reviewId)
    {
        var review = await dataContext.Reviews.FindAsync(reviewId);
        // var review = await dataContext.Reviews.FirstOrDefaultAsync(a=> a.Id == reviewId);
        if (review == null)
        {
            return TypedResults.NotFound();
        }
        var mappedReview = mapper.Map<ReviewDto>(review);
        return TypedResults.Ok(mappedReview);
    }
    
   
    public static async Task<Results<NotFound, CreatedAtRoute<ReviewDto>>> CreateReviewAsync(DataContext dataContext,
        IMapper mapper,
        CreateReviewDto createReviewDto)
    {
        var book = await dataContext.Books.FindAsync(createReviewDto.BookId);
        var user = await dataContext.Users.FindAsync(createReviewDto.UserId);
        if (book == null || user == null)
        {
            return TypedResults.NotFound();
        }
        var review = mapper.Map<Review>(createReviewDto);
        dataContext.Add(review);
        await dataContext.SaveChangesAsync();
    
        var reviewToReturn = mapper.Map<ReviewDto>(review);
        return TypedResults.CreatedAtRoute(
            reviewToReturn,
            "GetReview",
            new {reviewId=reviewToReturn.Id}
        );
    }
    
    public static async Task<Results<NotFound, NoContent>> EditReviewAsync (DataContext dataContext,
        IMapper mapper,
        [FromRoute] int reviewId,
        CreateReviewDto createReviewDto) 
    { 
        var review = await dataContext.Reviews.FirstOrDefaultAsync(d => d.Id == reviewId);
        if (review == null)
        {
            return TypedResults.NotFound();
        }
    
        mapper.Map(createReviewDto, review);
        // dataContext.Entry(review).State = EntityState.Modified;
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
    
    public static async Task<Results<NotFound, NoContent>> DeleteReviewAsync (DataContext dataContext, [FromRoute] int reviewId) 
    { 
        var review = await dataContext.Reviews.FindAsync(reviewId);
        // var review = await dataContext.Reviews.FirstOrDefaultAsync(a => a.Id == reviewId);
        if (review == null)
        {
            return TypedResults.NotFound();
        }
    
        // db.Reviews.Remove(review);
        dataContext.Remove(review);
        await dataContext.SaveChangesAsync();
    
        return TypedResults.NoContent();
    }
}