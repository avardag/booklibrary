using FluentValidation;

namespace NovusLiberus.Api.EndpointFilters;

public class ValidationFilterGeneric<T>:IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext, 
        EndpointFilterDelegate next)
    {
        var validator = efiContext.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator != null)
        {
            var entity = efiContext.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T) );
            if (entity != null)
            {
                var validation = await validator.ValidateAsync(entity);
                if (validation.IsValid)
                {
                    return await next.Invoke(efiContext);
                }
                return TypedResults.ValidationProblem(validation.ToDictionary());
            }
            else
            {
                return TypedResults.Problem(new()
                {
                    Status = 400,
                    Title = "Error occured",
                    Detail = "Could not find the entity to validate"
                }); 
            }
            
        }
        return await next.Invoke(efiContext);
    }
}