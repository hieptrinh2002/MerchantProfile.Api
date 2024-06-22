using System.ComponentModel.DataAnnotations;

namespace MerchantProfile.Api.Models.Dtos.Request
{
    public class CreatePromotionDto
    {
        public string Code { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateExpire { get; set; }

        public int QuantityAvailable { get; set; }

        [Required]
        public double Condition { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Discount must be between 1 and 100.")]
        public float Discount { get; set; } // % discount

        [Required]
        public string MerchantId { get; set; }
    }
}
