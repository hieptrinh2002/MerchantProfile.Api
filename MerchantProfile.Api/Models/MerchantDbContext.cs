using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

namespace MerchantProfile.Api.Models;
public partial class MerchantDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public MerchantDbContext(DbContextOptions<MerchantDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Merchant> Merchants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data
        modelBuilder.Entity<Merchant>().HasData(
            new Merchant
            {
                Name = "Merchant One",
                Phone = "123-456-7890",
                Email = "sample@gmail.com",
                Description = "First Merchant Description",
                LinkWebsite = "http://www.merchantone.com",
                Address = "123 Main St, Anytown, USA"
            },
            new Merchant
            {
                Name = "Merchant Two",
                Phone = "098-765-4321",
                Email = "sample2@gmail.com",
                Description = "Second Merchant Description",
                LinkWebsite = "http://www.merchanttwo.com",
                Address = "456 Elm St, Othertown, USA"
            }
        );
    }
}
