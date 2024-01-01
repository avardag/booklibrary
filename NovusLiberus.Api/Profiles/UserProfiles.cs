using AutoMapper;
using NovusLiberus.Api.DTOs.UserDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class UserProfiles:Profile
{
    public UserProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, UserDetailsDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.LibraryCardNumber,
                opt => opt.MapFrom((src,
                    dest,
                    destMember,
                    context) => context.Items["LibraryCardNumber"]));
        CreateMap<EditUserDto, User>();
    }
}