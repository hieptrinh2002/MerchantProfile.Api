using MerchantProfile.Api.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantProfile.Api.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
           
            try
            {
                if (context.Request.Path.StartsWithSegments("/api/merchant-profiles/login") ||
                    context.Request.Path.StartsWithSegments("/api/merchant-profiles/register"))
                {
                    await _next(context); // raise exception 
                    return;
                }

                if (context.Request.Cookies.TryGetValue("Signature", out var signature))
                {
                    var dbContext = serviceProvider.GetRequiredService<MerchantDbContext>();
                    var merchant = dbContext.Merchants.FirstOrDefault(m => m.signature == signature);

                    if (merchant != null)
                    {
                        await _next(context);
                        return;
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync("{\"message\": \"Unauthorized: Invalid merchant signature.\"}");
                        return;
                    }
                }

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"Unauthorized: Signature cookie not found.\"}");
            }
            catch (Exception ex)
            {
                ////Log exception or handle it as needed
                Console.WriteLine($"Exception caught in middleware: {ex.Message}");
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("{\"message\": \"Internal server error.\"}");
            }
        }
    }
}
