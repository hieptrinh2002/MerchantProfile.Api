using MerchantProfile.Api.Exceptions;
using MerchantProfile.Api.Helper;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;
using MerchantProfile.Api.Models.Enums;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MerchantProfile.Api.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PromotionService> _logger;
        public PromotionService(HttpClient httpClient, IConfiguration configuration, ILogger<PromotionService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> CreatePromotionAsync(CreatePromotionDto promotiondto)
        {
            var url = _configuration["promotion_api_url"];
            return await HttpHelper.PostAsync(_httpClient, url, promotiondto);
        }

        public async Task<GetListPromotionResponseDto?> GetAllPromotionByMerchantIdAsync(string merchantId)
        {
            var url = _configuration["promotion_api_url"] + $"/merchant/{merchantId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetListPromotionResponseDto apiResponse = JsonConvert.DeserializeObject<GetListPromotionResponseDto>(jsonStrResponse);
                return apiResponse;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting all promotions with merchantId: {MerchantId}, status: {status}",
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting all promotions with merchantId: {MerchantId}, status: {status}",
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }
            return null;
        }

        public async Task<GetPromotionByIdResponseDto?> GetPromotionByIdAsync(string promotionId)
        {
            var url = _configuration["promotion_api_url"] + $"/{promotionId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetPromotionByIdResponseDto apiResponse = JsonConvert.DeserializeObject<GetPromotionByIdResponseDto>(jsonStrResponse);

                return apiResponse;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting promotion with id : {promotionId}, status: {status}",
                                  promotionId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting promotion with id: {promotionId}, status: {status}",
                                  promotionId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }

            return null;
        }
    }
}
