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
        }
    }
}
