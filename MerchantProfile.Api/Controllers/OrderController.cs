using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MerchantProfile.Api.Services;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Authorization;

namespace MerchantProfile.Api.Controllers
{
    [Authorize]
    [Route("api/merchant-profiles/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private IActionResult Success(object data)
        {
            return Ok(new { status = "SUCCESS", messages = "success", data });
        }

        [HttpGet("today/{merchantId}")]
        public async Task<IActionResult> GetOrdersToDayAsync(string merchantId)
        {
            var orders = await _orderService.GetOrdersToDayAsync(merchantId);
            return Success(new { orders });
        }

        [HttpGet("month/{year}/{month}/{merchantId}")]
        public async Task<IActionResult> GetOrdersByMonthAsync(string merchantId, int month, int year)
        {
            var orders = await _orderService.GetOrdersByMonthAsync(merchantId, month, year);
            return Success(new { orders });
        }

        [HttpGet("year/{year}/{merchantId}/")]
        public async Task<IActionResult> GetOrdersByYearAsync(string merchantId, int year)
        {
            var orders = await _orderService.GetOrdersByYearAsync(merchantId, year);
            return Success(new { orders });
        }

        [HttpGet("quarter/{merchantId}/{quarter}/{year}")]
        public async Task<IActionResult> GetOrdersByQuarterAsync(string merchantId, int quarter, int year)
        {
            var orders = await _orderService.GetOrdersByQuarterAsync(merchantId, quarter, year);
            return Success(new { orders });
        }

        [HttpGet("events/{eventId}")]
        public async Task<IActionResult> GetOrdersByEventAsync(string eventId)
        {
            var orders = await _orderService.GetOrderOfEvent(eventId);
            return Success(new { orders });
        }
    }
}
