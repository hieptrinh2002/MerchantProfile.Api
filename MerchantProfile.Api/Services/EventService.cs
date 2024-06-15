using MerchantProfile.Api.Exceptions;
using MerchantProfile.Api.Helper;
using MerchantProfile.Api.Models.Dtos.Request;
using MerchantProfile.Api.Models.Dtos.Response;
using MerchantProfile.Api.Models.Enums;
using MerchantProfile.Api.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MerchantProfile.Api.Services
{
    public class EventService: IEventService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EventService> _logger;
        public EventService(HttpClient httpClient, IConfiguration configuration, ILogger<EventService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IActionResult> CreateEventAsync(CreateEventDto createRequest)
        {
            var url = _configuration["event_api_url"] + "/create";
            return await HttpHelper.PostAsync(_httpClient, url, createRequest);
        }

        public async Task<IActionResult> UpdateEventAsync(UpdateEventDto updateRequest)
        {
            var url = "http://localhost:8080/event/update";
            return await HttpHelper.PutAsync(_httpClient, url, updateRequest);
        }

        public async Task<GetListEventResponseDTo?> GetAllEventAsync(string merchantId)
        {
            var url = _configuration["event_api_url"] + $"/get-by-merchant/{merchantId}";
            var response = await _httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetListEventResponseDTo apiResponse = JsonConvert.DeserializeObject<GetListEventResponseDTo>(jsonStrResponse);
                return apiResponse;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting all events with merchantId: {MerchantId}, status: {status}",
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting all events with merchantId: {MerchantId}, status: {status}", 
                                  merchantId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }
            return null;
        }

        public async Task<GetEventByIdResponseDto?> GetEventByIdAsync(string eventId)
        {
            var url = _configuration["event_api_url"] + $"/{eventId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var jsonStrResponse = await response.Content.ReadAsStringAsync();
                GetEventByIdResponseDto apiResponse = JsonConvert.DeserializeObject<GetEventByIdResponseDto>(jsonStrResponse);

                return apiResponse;
            }
            else if ((int)response.StatusCode >= 400 && (int)response.StatusCode < 500)
            {
                _logger.LogError("Client error when getting event with eventId: {eventId}, status: {status}",
                                  eventId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.CLIENT_ERROR);
            }
            else if ((int)response.StatusCode >= 500)
            {
                _logger.LogError("Server error when getting events with eventId: {eventId}, status: {status}",
                                  eventId, (int)response.StatusCode);
                throw new DefaultException(APIStatus.SERVER_ERROR);
            }

            return null;
        }

    }
}
