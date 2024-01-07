using NovusLiberus.Api.Constants;
using NovusLiberus.Api.DTOs.ReviewDtos;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class ReviewEndpointRouteBuilderExtension
{
    public static void MapReviewsEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Endpoint Groups//
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup(RouteConstants.ApiV1);
        var reviewsEndpoints = apiVersionOneEndpoints.MapGroup("/reviews")
            .WithTags(nameof(Review) + " routes")
            .WithOpenApi();
        var reviewWithIdEndpoints = reviewsEndpoints.MapGroup("/{reviewId:int}");
        var reviewWithBookIdEndpoints = reviewsEndpoints.MapGroup("/book/{bookId:int}");

        //Endpoints//
        reviewWithBookIdEndpoints.MapGet("/", ReviewsHandlers.GetAllReviewsForBookAsync)
            .WithName("GetReviews")
            .WithSummary("Gets all reviews for a book");
        reviewWithIdEndpoints.MapGet("/", ReviewsHandlers.GetReviewByIdAsync)
            .WithName("GetReview")
            .WithSummary("Gets review by providing ID");
        reviewsEndpoints.MapPost("/", ReviewsHandlers.CreateReviewAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateReviewDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Creates new review by providing review DTO");
        reviewWithIdEndpoints.MapPut("/", ReviewsHandlers.EditReviewAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateReviewDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Edits, updates review by providing ID");
        reviewWithIdEndpoints.MapDelete("/", ReviewsHandlers.DeleteReviewAsync)
            .WithSummary("Deletes review by providing ID");
    }
}