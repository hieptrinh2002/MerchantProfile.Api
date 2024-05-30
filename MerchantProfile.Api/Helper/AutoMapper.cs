using AutoMapper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;

namespace MerchantProfile.Api.Helper
{
    public class AutoMapper: Profile    
    {
        public AutoMapper()
        {
            CreateMap<Merchant, MerchantDto>().ReverseMap();
            //CreateMap<Merchant, MerchantRegistrationDto>()
            //       .ForMember(dest => dest.Password, opt => opt.Ignore());
            //CreateMap<MerchantRegistrationDto, Merchant>()
            //    .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            //    .ForMember(dest => dest.signature, opt => opt.Ignore());

        }
    }
}
