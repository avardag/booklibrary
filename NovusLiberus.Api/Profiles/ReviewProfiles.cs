using AutoMapper;
using NovusLiberus.Api.DTOs.ReviewDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class ReviewProfiles:Profile
{
    public ReviewProfiles()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewDto, Review>();
        CreateMap<EditReviewDto, Review>();
    }
}