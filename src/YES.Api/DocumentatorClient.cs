using YES.Dto;
using Shouldly;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace YES;

public interface IYESClient
{
    void Authenticate(CancellationToken token = default);
    Task<JsonElement> Document(DocumentMyCodeDto request, CancellationToken token = default);
}

public class YESClient : IYESClient
{
    readonly IHttpClient _httpClient;
    public YESClient(IHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public void Authenticate(CancellationToken token = default)
    {
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("OpenApiKey"));
    }

    public async Task<JsonElement> Document(DocumentMyCodeDto request, CancellationToken token = default)
    {
        var uri = "/v1/chat/completions";
        var req = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(request, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }), Encoding.UTF8, "application/json")
        };
        var res = await _httpClient.SendAsync(req);
        res.StatusCode.ShouldBe(HttpStatusCode.OK);
        return JsonSerializer.Deserialize<JsonElement>(await res.Content.ReadAsStringAsync());
    }
}