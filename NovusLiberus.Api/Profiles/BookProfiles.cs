using AutoMapper;
using NovusLiberus.Api.DTOs.BookDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class BookProfiles:Profile
{
    public BookProfiles()
    {
        CreateMap<Book, BookWithAuthorsAndGenresDto>();
        CreateMap<Book, BookDto>();
            // .ForMember(b => b.Authors.);
            // .IncludeMembers(a => a.Authors)
            // .IncludeMembers(b => b.Genres);
        CreateMap<CreateBookDto, Book>();
        CreateMap<EditBookDto, Book>();
    }
}