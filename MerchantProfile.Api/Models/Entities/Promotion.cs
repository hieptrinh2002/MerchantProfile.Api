using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MerchantProfile.Api.Models.Entities;

public class Promotion
{
    public string Id { get; set; }
    public string Code { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime DateExpire { get; set; }

    public int QuantityAvailable { get; set; }

    public double Condition { get; set; }

    public float Discount { get; set; }

    public string MerchantId { get; set; }
}
