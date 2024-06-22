namespace MerchantProfile.Api.Models.Dtos.Response
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
