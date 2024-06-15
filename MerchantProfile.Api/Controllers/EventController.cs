using AutoMapper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MerchantProfile.Api.Controllers
{
    [Route("api/merchant-profiles/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly MerchantDbContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        public EventController(MerchantDbContext context, IMapper mapper, IEventService eventService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDto eventRequest)
        {
            return await _eventService.CreateEventAsync(eventRequest);
        }

        [HttpGet("{merchantId}")]

        public async Task<IActionResult> getAllEvent(string merchantId)
        {
            return await _eventService.GetAllEventAsync(merchantId);
        }

        [HttpGet("test/{id}")]

        public async Task<IActionResult> getTesst(string id)
        {
            return await _eventService.GetTestAsync(id);
        }

        [HttpPost("test")]
        public async Task<IActionResult> test([FromBody] PostDto dto)
        {
            var result =  await _eventService.CreateTestAsync(dto);
            return result;
        }
    }
}
