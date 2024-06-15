using System.Security.Claims;
using MerchantProfile.Api.Models.Entities;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;

namespace MerchantProfile.Api.Services.IServices
{
    public interface IAuthenService
    {
        Task<(bool IsSuccess, string Message, TokenResponse Data)> LoginAsync(LoginDto dto);
        IEnumerable<Claim> GetClaims(Merchant merchant);
        (string token, DateTime expire, string tokenId) GenerateToken(IEnumerable<Claim> claims);
    }
}
