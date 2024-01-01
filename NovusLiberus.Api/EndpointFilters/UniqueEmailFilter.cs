using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs.UserDtos;

namespace NovusLiberus.Api.EndpointFilters;

public class UniqueEmailFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext, 
        EndpointFilterDelegate next)
    {
        // string emailFromBody ="";
        // if (efiContext.HttpContext.Request.Method == "PUT")
        // {
        //     var user = efiContext.Arguments.FirstOrDefault(o => o.GetType() == typeof(EditUserDto)) as EditUserDto;
        //     emailFromBody = user?.Email ?? ""; 
        // }
        // else if (efiContext.HttpContext.Request.Method == "POST")
        // {
        //     var user = efiContext.Arguments.FirstOrDefault(o => o.GetType() == typeof(CreateUserDto)) as CreateUserDto;
        //     emailFromBody = user?.Email ?? "";
        // }

        var user = efiContext.Arguments.FirstOrDefault(o => o.GetType() == typeof(CreateUserDto)) as CreateUserDto;
        string  emailFromBody = user?.Email ?? "";
        
        if (emailFromBody.Length > 0)
        {
           var db = efiContext.HttpContext.RequestServices.GetService<DataContext>();
           var userFromDb = await db.Users.FirstOrDefaultAsync(u=> u.Email == emailFromBody);
           if ( userFromDb != null)
           {
               return TypedResults.Problem(new()
               {
                   Status = 400,
                   Title = "Error adding email",
                   Detail = "Please choose another email"
               });
           } 
        }
        
        //invoke the next filter
        var result = await next.Invoke(efiContext);
        return result;
    }
}