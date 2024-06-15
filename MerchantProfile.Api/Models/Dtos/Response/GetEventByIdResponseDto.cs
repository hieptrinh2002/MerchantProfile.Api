using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetEventByIdResponseDto
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public Event Data { get; set; }
    }
}
