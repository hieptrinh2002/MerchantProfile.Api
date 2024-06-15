using MerchantProfile.Api.Models.Entities;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class UpdateEventResponseDto
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public UpdatedEvent Data { get; set; }
    }

    public class UpdatedEvent
    {
        public Event Event { get; set; }
    }
}
