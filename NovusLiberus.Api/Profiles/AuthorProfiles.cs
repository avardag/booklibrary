using AutoMapper;
using NovusLiberus.Api.DTOs.AuthorDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class AuthorProfiles:Profile
{
    public AuthorProfiles()
    {
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorDetailsDto>();
        CreateMap<CreateAuthorDto, Author>();
        // CreateMap<DishForUpdateDto, Dish>();
    }
}