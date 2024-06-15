using MerchantProfile.Api.Models.Enums;

namespace MerchantProfile.Api.Models.Dtos
{
    public class OrderDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string MerchantId { get; set; }
        public string EventId { get; set; }
        public long Amount { get; set; }
        public OrderStatus Status { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public string CreatedDate { get; set; }
        public string ExpiredDate { get; set; }
        public string PromotionId { get; set; }
        public string StartEventDate { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
    }
}
