using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetPromotionByIdResponseDto
    {

        public string Status { get; set; }

        public string Message { get; set; }

        public Promotion Data { get; set; }
    }
}
