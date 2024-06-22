using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Models.Dtos.Request;
using Microsoft.AspNetCore.Mvc;
using MerchantProfile.Api.Models.Dtos.Response;

namespace MerchantProfile.Api.Services.IServices
{
    public interface IPromotionService
    {
        Task<IActionResult> CreatePromotionAsync(CreatePromotionDto promotiondto);

        Task<GetListPromotionResponseDto?> GetAllPromotionByMerchantIdAsync(string merchantId);

        Task<GetPromotionByIdResponseDto> GetPromotionByIdAsync(string eventId);
    }
}
