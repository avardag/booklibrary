namespace NovusLiberus.Api.EndpointFilters;

public class AuthorIsLockedFilter:IEndpointFilter
{
    private readonly int _lockedAuthorId;
    public AuthorIsLockedFilter(int lockedAuthorId)
    {
        _lockedAuthorId = lockedAuthorId;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext, 
        EndpointFilterDelegate next)
    {
        // context.HttpContext.GetRouteValue();
        // var dishId = efiContext.GetArgument<int>(2);//int in delete handler and put handler have different indexes
        
        // var authorId = efiContext.Arguments.FirstOrDefault(a => a.GetType() == typeof(int)) as int?;
        // if ( authorId!=null && authorId == _lockedAuthorId)
        // {
        //use pattern matching instead:
        if ( efiContext.Arguments.FirstOrDefault(a => a.GetType() == typeof(int)) is int authorId && (int?)authorId == _lockedAuthorId)
        {
            return TypedResults.Problem(new()
            {
                Status = 400,
                Title = "Author cant be modified",
                Detail = "You can not update or delete the perfect author"
            });
        }
        //invoke the next filter
        var result = await next.Invoke(efiContext);
        return result;
    }
}