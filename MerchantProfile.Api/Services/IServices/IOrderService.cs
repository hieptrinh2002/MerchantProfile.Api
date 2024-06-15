using MerchantProfile.Api.Models.Dtos;
using MerchantProfile.Api.Models.Dtos.Response;

namespace MerchantProfile.Api.Services.IServices
{
    public interface IOrderService
    {
        Task<GetOrderResponseDto> GetAllOrderAsync(string merchantId);
        Task<List<OrderDto>> GetOrdersToDayAsync(string merchantId);
        Task<List<OrderDto>> GetOrdersByMonthAsync(string merchantId, int month, int year);
        Task<List<OrderDto>> GetOrdersByYearAsync(string merchantId, int year);
        Task<List<OrderDto>> GetOrdersByQuarterAsync(string merchantId, int quarter, int year);
        Task<List<OrderDto>> GetOrderOfEvent(string eventId);
    }
}
