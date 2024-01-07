using NovusLiberus.Api.Constants;
using NovusLiberus.Api.DTOs.LoanDtos;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class LoanEndpointRouteBuilderExtension
{
    public static void MapLoansEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Endpoint Groups//
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup(RouteConstants.ApiV1);
        var loansEndpoints = apiVersionOneEndpoints.MapGroup("/loans")
            .WithTags(nameof(Loan) + " routes")
            .WithOpenApi();
        var loanWithIdEndpoints = loansEndpoints.MapGroup("/{loanId:int}");
        var loanWithUserIdEndpoints = loansEndpoints.MapGroup("/user/{userId:int}");

        //Endpoints//
        loanWithUserIdEndpoints.MapGet("/", LoansHandlers.GetAllLoansForUserAsync)
            .WithName("GetLoans")
            .WithSummary("Gets all loans for a user by providing a ser Id as a route parameter")
            .WithDescription(
                "Gets all borrowings for a user, old and active. Optionally a query parameter onlyActive" 
                + " can be passed to a route to return only active loans, i.e. books still out on loan for a particular user");
        loanWithIdEndpoints.MapGet("/", LoansHandlers.GetLoanByIdAsync)
            .WithName("GetLoan")
            .WithSummary("Gets loan by providing ID");
        loanWithIdEndpoints.MapPatch("/", LoansHandlers.CloseALoanAsync)
            .WithSummary("Return a book, close loan by providing loan ID");
        loansEndpoints.MapPost("/", LoansHandlers.CreateLoanAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateLoanDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Creates new loan by providing loan DTO");
        loansEndpoints.MapPost("/new", LoansHandlers.CreateLoanWithQueryParamsAsync)
            .WithSummary("Creates new loan by providing query params for userId and bookId");
        loanWithIdEndpoints.MapDelete("/", LoansHandlers.DeleteLoanAsync)
            .WithSummary("Deletes loan by providing ID. Don't use this endpoint")
            .WithOpenApi(operation => new(operation)
            {
                Deprecated = true
            });
    }
}