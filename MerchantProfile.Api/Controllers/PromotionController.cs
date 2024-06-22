using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MerchantProfile.Api.Controllers
{
    [Authorize]
    [Route("api/merchant-profiles")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpPost("promotions/create")]
        public async Task<IActionResult> CreatePromotion([FromBody] CreatePromotionDto promotionRequest)
        {
            return await _promotionService.CreatePromotionAsync(promotionRequest);
        }

        [HttpGet("{merchantId}/promotions")]

        public async Task<IActionResult> getAllPromotion(string merchantId)
        {
            //check promotion có phải do merchant này tạo không? => /....

            var response = await _promotionService.GetAllPromotionByMerchantIdAsync(merchantId);
            return Ok(response);
        }

        [HttpGet("promotions/{promotionId}")]

        public async Task<IActionResult> getPromotionById(string promotionId)
        {
            var response = await _promotionService.GetPromotionByIdAsync(promotionId);
            return Ok(response);
        }
    }
}
