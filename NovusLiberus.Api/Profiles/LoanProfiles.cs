using AutoMapper;
using NovusLiberus.Api.DTOs.LoanDtos;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Profiles;

public class LoanProfiles :Profile
{
public LoanProfiles()
{
    CreateMap<Loan, LoanDto>();
    CreateMap<Loan, LoanDetailsDto>();
    CreateMap<Loan, LoanWithBookInfoDto>();
    CreateMap<CreateLoanDto, Loan>();
}
}