using System.ComponentModel.DataAnnotations;

namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class GetMerchantByIdResponseDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public string? Description { get; set; }

        public string? LinkWebsite { get; set; }

        public string Address { get; set; }

    }
}
