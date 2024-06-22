using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MerchantProfile.Api.Controllers
{
    [Authorize]
    [Route("api/merchant-profiles")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("events/create")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventRequest)
        {
            return await _eventService.CreateEventAsync(eventRequest);
        }

        [HttpPut("events/update")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto eventRequest)
        {
            return await _eventService.UpdateEventAsync(eventRequest);
        }

        [HttpGet("{merchantId}/events")]

        public async Task<IActionResult> getAllEvent(string merchantId)
        {
            //check event có phải do merchant này tạo không? => /....
            var merchantIdClaim = User.Claims.FirstOrDefault(c => c.Type == "MerchantId");


            var response = await _eventService.GetAllEventAsync(merchantId);
            return Ok(response);
        }

        [HttpGet("events/{eventId}")]

        public async Task<IActionResult> getEventById(string eventId)
        {
            var response = await _eventService.GetEventByIdAsync(eventId);
            return Ok(response);
        }
    }
}
