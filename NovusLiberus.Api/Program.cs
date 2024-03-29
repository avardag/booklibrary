using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Data;
using NovusLiberus.Api.DTOs;
using NovusLiberus.Api.DTOs.AuthorDtos;
using NovusLiberus.Api.RouteBuilderExtensions;

// DotNetEnv.Env.Load(".env");
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
builder.Services.ConfigureHttpJsonOptions(opts =>
{
    opts.SerializerOptions.IncludeFields = true;
    opts.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
//DB
builder.Services.AddDbContext<DataContext>(options =>
{
    // options.UseSqlServer(builder.Configuration.GetConnectionString("NovusLiberusDbConnection"))
    options.UseSqlServer(DotNetEnv.Env.GetString("AZURE_DB_CONNECTION_STRING"))
        .EnableSensitiveDataLogging();
    // .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//add fluent validation for assemblies containing CreateAuthorDto
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateAuthorDto));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapAuthorsEndpoints();
app.MapUsersEndpoints();
app.MapGenresEndpoints();
app.MapBooksEndpoints();
app.MapReviewsEndpoints();
app.MapLoansEndpoints();


app.Run();
