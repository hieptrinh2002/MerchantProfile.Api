using MerchantProfile.Api.Models.Entities;
using Newtonsoft.Json;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetListEventResponseDTo
    {
        public string Status { get; set; }

        public string Message { get; set; }

        public EventData Data { get; set; }
    }

    public class EventData
    {
        public List<Event> Events { get; set; }
    }
}
