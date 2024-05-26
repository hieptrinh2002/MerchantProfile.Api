using MerchantProfile.Api.Helper;
using MerchantProfile.Api.Models;
using MerchantProfile.Api.Models.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MerchantProfile.Api.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EventService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> CreateEventAsync(EventDto eventRequest)
        {
            var url = _configuration["event_api_url"] + "/create";
            return await HttpHelper.PostJsonAsync(_httpClient, url, eventRequest);
        }

        public async Task<IActionResult> GetAllEventAsync(string merchantId)
        {
            var url = _configuration["event_api_url"] + $"/{merchantId}";
            return await HttpHelper.GetAsync(_httpClient, url);
        }

        public async Task<IActionResult> GetTestAsync(string merchantId)
        {
            var url = $"https://jsonplaceholder.typicode.com/todos/{merchantId}";
            return await HttpHelper.GetAsync(_httpClient, url);
        }

        public async Task<IActionResult> CreateTestAsync(PostDto eventRequest)
        {
            var url = "https://jsonplaceholder.typicode.com/posts";
            var resul =  await HttpHelper.PostJsonAsync(_httpClient, url, eventRequest);
            return resul;
        }

    }
}
