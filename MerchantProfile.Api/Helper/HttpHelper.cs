using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Ocsp;
using MerchantProfile.Api.Models.Dtos;
using System.Net.Http.Headers;

namespace MerchantProfile.Api.Helper;

public static class HttpHelper
{
    public static async Task<IActionResult> SendJsonAsync<T>(HttpClient httpClient, string url, T requestData, HttpMethod method)
    {
        var jsonContent = JsonSerializer.Serialize(requestData);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Tạo HttpRequestMessage với method và URL được chỉ định
        var request = new HttpRequestMessage(method, url) { Content = content };

        // Gửi request
        var response = httpClient.Send(request);

        // Xử lý response
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

    // POST
    public static Task<IActionResult> PostAsync<T>(HttpClient httpClient, string url, T requestData)
    {
        return SendJsonAsync(httpClient, url, requestData, HttpMethod.Post);
    }

    // PUT
    public static Task<IActionResult> PutAsync<T>(HttpClient httpClient, string url, T requestData)
    {
        return SendJsonAsync(httpClient, url, requestData, HttpMethod.Put);
    }

    // PATCH
    public static Task<IActionResult> PatchAsync<T>(HttpClient httpClient, string url, T requestData)
    {
        return SendJsonAsync(httpClient, url, requestData, HttpMethod.Patch);
    }
}
