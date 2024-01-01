using AutoMapper;
using NovusLiberus.Api.DTOs.GenreDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class GenreProfiles :Profile
{
    public GenreProfiles()
    {
        CreateMap<Genre, GenreDto>();
        CreateMap<CreateGenreDto, Genre>();
    }
}