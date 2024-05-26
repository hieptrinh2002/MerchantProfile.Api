using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MerchantProfile.Api.Services;

public interface IEventService
{
    Task<IActionResult> CreateEventAsync(EventDto eventRequest);

    Task<IActionResult> GetAllEventAsync(string merchantId);

    Task<IActionResult> GetTestAsync(string merchantId);

    Task<IActionResult> CreateTestAsync(PostDto eventRequest);
}
