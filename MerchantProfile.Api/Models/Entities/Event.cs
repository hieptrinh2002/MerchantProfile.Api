namespace MerchantProfile.Api.Models.Entities;

public class Event
{
    public string Id { get; set; }
    public string MerchantId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int MinQuantity { get; set; }
    public int MaxQuantity { get; set; }
    public string Location { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public string Type { get; set; }
    public bool IsUnavailable { get; set; }
    public bool IsOutOfStock { get; set; }
}