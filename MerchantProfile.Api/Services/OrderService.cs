using MerchantProfile.Api.Exceptions;
using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Models.Dtos.Response;
using MerchantProfile.Api.Models.Entities;
using MerchantProfile.Api.Models.Enums;
using MerchantProfile.Api.Services.IServices;
using Newtonsoft.Json;
using System.Net.Http;

namespace MerchantProfile.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<OrderService> _logger;
        public OrderService(HttpClient httpClient, IConfiguration configuration, ILogger<OrderService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GetOrderResponseDto> GetAllOrderAsync(string merchantId)
        {
            var url = $"{_configuration["order_api_url"]}/merchant/{merchantId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetOrderResponseDto apiResponse = JsonConvert.DeserializeObject<GetOrderResponseDto>(jsonStrResponse);
                return apiResponse;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting all orders with merchantId: {MerchantId}, status: {status}",
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting all orders with merchantId: {MerchantId}, status: {status}",
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }
            return null;
        }
        public async Task<List<OrderDto>> GetOrdersToDayAsync(string merchantId)
        {
            var allOrders = await GetAllOrderAsync(merchantId);
            var resut = allOrders.Data.Orders.Where(o => DateTime.Parse(o.CreatedDate).Date == DateTime.Today).ToList();

            return allOrders.Data.Orders.Where(o => DateTime.Parse(o.CreatedDate).Date == DateTime.Today).ToList();
        }

        public async Task<List<OrderDto>> GetOrdersByMonthAsync(string merchantId, int month, int year)
        {
            var allOrders = await GetAllOrderAsync(merchantId);

            return allOrders.Data.Orders.Where(o => DateTime.Parse(o.CreatedDate).Month == month &&
                                                      DateTime.Parse(o.CreatedDate).Year == year)
                                          .ToList();
        }

        public async Task<List<OrderDto>> GetOrdersByYearAsync(string merchantId, int year)
        {
            var allOrders = await GetAllOrderAsync(merchantId);
            return allOrders.Data.Orders.Where(o => DateTime.Parse(o.CreatedDate).Year == year).ToList();
        }

        public async Task<List<OrderDto>> GetOrdersByQuarterAsync(string merchantId, int quarter, int year)
        {
            var allOrders = await GetAllOrderAsync(merchantId);

            // Lọc các đơn hàng trong quý
            var orders = allOrders.Data.Orders.Where(u => DateTime.Parse(u.CreatedDate).Year == year).ToList();
            return quarter switch
            {
                1 => orders.Where(o => DateTime.Parse(o.CreatedDate).Month >= 1 && DateTime.Parse(o.CreatedDate).Month <= 3)
                           .ToList(),
                2 => orders.Where(o => DateTime.Parse(o.CreatedDate).Month >= 4 && DateTime.Parse(o.CreatedDate).Month <= 6)
                           .ToList(),
                3 => orders.Where(o => DateTime.Parse(o.CreatedDate).Month >= 7 && DateTime.Parse(o.CreatedDate).Month <= 9)
                           .ToList(),
                4 => orders.Where(o => DateTime.Parse(o.CreatedDate).Month >= 10 && DateTime.Parse(o.CreatedDate).Month <= 12)
                           .ToList(),
                _ => new List<OrderDto>()  // Trường hợp không hợp lệ
            };
        }

        public async Task<List<OrderDto>> GetOrderOfEvent(string eventId)
        {
            var url = $"{_configuration["event_api_url"]}/get-by-event/{eventId}";

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetOrderResponseDto apiResponse = JsonConvert.DeserializeObject<GetOrderResponseDto>(jsonStrResponse);
                return apiResponse.Data.Orders;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting all orders of event : {eventId}, status: {status}",
                                  eventId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting all orders of event : {eventId}, status: {status}",
                                  eventId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }
            return null;
        }
    }
}
