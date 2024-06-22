using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetListPromotionResponseDto
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public List<Promotion> Data { get; set; }
    }
}
