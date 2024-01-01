using NovusLiberus.Api.Constants;
using NovusLiberus.Api.DTOs;
using NovusLiberus.Api.DTOs.UserDtos;
using NovusLiberus.Api.EndpointFilters;
using NovusLiberus.Api.EndpointHandlers;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.RouteBuilderExtensions;

public static class UserEndpointRouteBuilderExtension
{
    public static void MapUsersEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        //Endpoint Groups//
        var apiVersionOneEndpoints = endpointRouteBuilder.MapGroup(RouteConstants.ApiV1);
        var usersEndpoints = apiVersionOneEndpoints.MapGroup("/users")
            .WithTags(nameof(User) + " routes")
            .WithOpenApi();
        var userWithIdEndpoints = usersEndpoints.MapGroup("/{userId:int}");
        // .RequireAuthorization();
        var usersWithIntIdAndLockFilters = usersEndpoints.MapGroup("/{userId:int}");

        //Endpoints//
        usersEndpoints.MapGet("/", UsersHandlers.GetAllUsersAsync)
            .WithName("GetUsers")
            .WithSummary("Gets all users");
        userWithIdEndpoints.MapGet("/", UsersHandlers.GetUserByIdAsync)
            .WithName("GetUser")
            .WithSummary("Gets user by providing ID");
        usersEndpoints.MapGet("/{userName}", UsersHandlers.GetUserByNameAsync)
            .AllowAnonymous()
            .WithSummary("Gets user by providing name")
            .WithOpenApi(operation =>
            {
                operation.Deprecated = true;
                return operation;
            });
        usersEndpoints.MapPost("/", UsersHandlers.CreateUserAsync)
            .AddEndpointFilter<ValidationFilterGeneric<CreateUserDto>>()
            .AddEndpointFilter<UniqueEmailFilter>()
            .ProducesValidationProblem(400)
            .WithSummary("Creates new user by providing user DTO")
            // .RequireAuthorization(policy =>
            // {
            //     policy.RequireRole("admin");
            // })
            ;
        //userWithIdEndpoints.MapPut("/", UsersHandlers.EditUserAsync)
        //    .AddEndpointFilter<ValidationFilterGeneric<CreateUserDto>>();
        //userWithIdEndpoints.MapDelete("/", UsersHandlers.DeleteUserAsync);
        //    .AddEndpointFilter<AlexIsLocked>()
        usersWithIntIdAndLockFilters.MapPut("/", UsersHandlers.EditUserAsync)
            .AddEndpointFilter<ValidationFilterGeneric<EditUserDto>>()
            .ProducesValidationProblem(400)
            .WithSummary("Edits, updates user by providing ID");
        usersWithIntIdAndLockFilters.MapDelete("/", UsersHandlers.DeleteUserAsync)
            .WithSummary("Deletes user by providing ID");
    }
}