using System.ComponentModel.DataAnnotations;

namespace MerchantProfile.Api.Models;

public class Event
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string merchantId { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string startDate { get; set; }

    public string endDate { get; set; }

    public string location { get; set; }

    public string type { get; set; }

    public int minQuantity { get; set; }

    public int maxQuantity { get; set; }

    public int price { get; set; }

    public string currency { get; set; }

}
