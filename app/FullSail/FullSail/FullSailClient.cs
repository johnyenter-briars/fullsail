using FullSail.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullSail;
public class FullSailClient
{

    private string _apiKey = "";
    private string _hostName = "192.168.0.8";
    private int _port = 8082;
    private static readonly HttpClient _httpClient = new()
    {
        Timeout = new TimeSpan(0, 0, 10),
    };
    private static readonly JsonSerializerSettings JsonSettings =
            new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };

    public FullSailClient()
    {
    }
    private async Task<TResponse> FullSailRequest<TResponse>(string path, HttpMethod httpMethod)
    {
        var request = new HttpRequestMessage(httpMethod, $"http://{_hostName}:{_port}/api/" + path);
        request.Headers.Accept.Clear();
        //request.Headers.Add("x-api-key", _apiKey);
        //request.Headers.Add("x-user-id", _userId);

        return await SendRequest<TResponse>(request);
    }
    private async Task<TResponse> FullSailRequest<TRequest, TResponse>(TRequest requestObject, string path, HttpMethod httpMethod)
    {
        var request = new HttpRequestMessage(httpMethod, $"http://{_hostName}:{_port}/api/" + path);
        request.Headers.Accept.Clear();
        //request.Headers.Add("x-api-key", _apiKey);
        //request.Headers.Add("x-user-id", _userId);

        request.Content = new StringContent(JsonConvert.SerializeObject(requestObject, JsonSettings), Encoding.UTF8, "application/json");

        return await SendRequest<TResponse>(request);
    }
    private async Task<TResponse> SendRequest<TResponse>(HttpRequestMessage request)
    {
        var clientResponse = await _httpClient.SendAsync(request, CancellationToken.None);

        if (clientResponse.IsSuccessStatusCode)
        {
            var str = await clientResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(str) ?? throw new NullReferenceException("JsonConvert.DeserializeObject<TResponse>");
        }
        else
        {
            var message = $"Failure to complete action. Reason phrase: {clientResponse.ReasonPhrase}, Raw response: {await clientResponse.Content.ReadAsStringAsync()}, Endpoint: {request.RequestUri}";
            throw new Exception(message);
        }

    }
    public async Task<List<MediaFile>> GetMediaFilesInStore(bool getDuration = false)
    {
        return await FullSailRequest<List<MediaFile>>($"media/list?duration={getDuration}", HttpMethod.Get);
    }
    public async Task<List<MediaFile>> GetMediaFilesInMediaSystem()
    {
        return await FullSailRequest<List<MediaFile>>($"media-system/list", HttpMethod.Get);
    }
    public async Task<List<SearchResult>> GetTorrentSearchResults(string query, SearchWebsite searchWebsite = SearchWebsite.solid)
    {
        var encodedQuery = query.Replace(" ", "+").ToString();

        return await FullSailRequest<List<SearchResult>>($"search/{searchWebsite}/{encodedQuery}", HttpMethod.Get);
    }
    public FullSailClient UpdateSettings(string hostname, int port, string apiKey)
    {
        _hostName = hostname;
        _port = port;
        _apiKey = apiKey;
        return this;
    }
}
