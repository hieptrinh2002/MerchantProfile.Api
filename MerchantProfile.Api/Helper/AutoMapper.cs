using AutoMapper;
using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;
using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Helper
{
    public class AutoMapper: Profile    
    {
        public AutoMapper()
        {
            CreateMap<Merchant, MerchantDto>().ReverseMap();
            CreateMap<Merchant, RegisterDto>().ReverseMap();
            CreateMap<Merchant, GetMerchantByIdResponseDto>().ReverseMap();
        }
    }
}
