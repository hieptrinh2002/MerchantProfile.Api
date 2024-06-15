using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Models;
using System.Security.Claims;

namespace MerchantProfile.Api.Services.IServices
{
    public interface IAuthenService
    {
        Task<(bool IsSuccess, string Message, object Data)> LoginAsync(LoginDto dto);
        IEnumerable<Claim> GetClaims(Merchant merchant);
        (string token, DateTime expire, string tokenId) GenerateToken(IEnumerable<Claim> claims);
    }
}
