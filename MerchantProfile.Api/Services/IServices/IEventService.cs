using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace MerchantProfile.Api.Services.IServices;

public interface IEventService
{
    Task<IActionResult> CreateEventAsync(CreateEventDto eventRequest);

    Task<IActionResult> UpdateEventAsync(UpdateEventDto updateRequest);

    Task<GetListEventResponseDTo?> GetAllEventAsync(string merchantId);

    Task<GetEventByIdResponseDto> GetEventByIdAsync(string eventId);

}
