using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;

namespace MerchantProfile.Api.Helper;

public static class HttpHelper
{
    public static async Task<IActionResult> PostJsonAsync<T>(HttpClient httpClient, string url, T requestData)
    {
        var jsonContent = JsonSerializer.Serialize(requestData);
        var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        if (response.IsSuccessStatusCode)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(responseData);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new ObjectResult(errorContent) { StatusCode = (int)response.StatusCode };
        }
    }

    public static async Task<IActionResult> GetAsync(HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            response.EnsureSuccessStatusCode();
            string responseData = await response.Content.ReadAsStringAsync();
            return new OkObjectResult(responseData);
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            return new ObjectResult(errorContent) { StatusCode = (int)response.StatusCode };
        }
    }
}
