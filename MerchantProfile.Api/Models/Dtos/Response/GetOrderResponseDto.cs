using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetOrderResponseDto
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public OrderData Data { get; set; }
    }
    public class OrderData
    {
        public List<OrderDto> Orders { get; set; }
    }
}
