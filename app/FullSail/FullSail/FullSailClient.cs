using FullSail.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FullSail;
public class FullSailClient
{
    private string _apiKey = "";
    private string _hostName = "";
    private int _port = 8082;
    private static HttpClientHandler Handler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
    private static readonly HttpClient _httpClient = new HttpClient(Handler)
    {
        Timeout = new TimeSpan(0, 1, 100),
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
        var request = new HttpRequestMessage(httpMethod, $"https://{_hostName}:{_port}/api/" + path);
        request.Headers.Accept.Clear();
        request.Headers.Add("x-api-key", _apiKey);
        //request.Headers.Add("x-user-id", _userId);

        return await SendRequest<TResponse>(request);
    }
    private async Task<TResponse> FullSailRequest<TRequest, TResponse>(TRequest requestObject, string path, HttpMethod httpMethod)
    {
        var request = new HttpRequestMessage(httpMethod, $"https://{_hostName}:{_port}/api/" + path);
        request.Headers.Accept.Clear();
        request.Headers.Add("x-api-key", _apiKey);
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
    public async Task<List<MediaFile>> GetMediaFilesInFolder(string folderPath = "media-root")
    {
        var folderSlashRemoved = folderPath.First() == '/' ? folderPath.Substring(1, folderPath.Length) : folderPath;
        var encodedFolder = folderSlashRemoved.Replace("/", "%2F");

        return await FullSailRequest<List<MediaFile>>($"media/list/{encodedFolder}", HttpMethod.Get);
    }
    public async Task<List<MediaFile>> GetMediaFilesInMediaSystem()
    {
        return await FullSailRequest<List<MediaFile>>($"media-system/list", HttpMethod.Get);
    }
    public async Task<List<TorrentSearchResult>> GetTorrentSearchResults(string query, TorrentSearchWebsite searchWebsite = TorrentSearchWebsite.solid)
    {
        var encodedQuery = query.Replace(" ", "+").ToString();

        return await FullSailRequest<List<TorrentSearchResult>>($"search/{searchWebsite}/{encodedQuery}", HttpMethod.Get);
    }
    public async Task<UpdateTorrentsResponse> StartTorrent(string magnetLink)
    {
        var body = new AddTorrentsRequest
        {
            MagnetLinks = new List<string> { magnetLink },
        };

        return await FullSailRequest<AddTorrentsRequest, UpdateTorrentsResponse>(body, $"torrents/add", HttpMethod.Post);
    }
    public async Task<UpdateFileMediaSystemResponse> DeleteFileInMediaSystem(string fileName)
    {
        var body = new UpdateFileMediaSystemRequest
        {
            FileName = fileName,
        };

        return await FullSailRequest<UpdateFileMediaSystemRequest, UpdateFileMediaSystemResponse>(body, $"media-system/delete", HttpMethod.Delete);
    }
    public async Task<UpdateFileMediaSystemResponse> SendFile(string fileNamePlusDirectory)
    {
        var body = new UpdateFileMediaSystemRequest
        {
            FileName = fileNamePlusDirectory,
        };

        return await FullSailRequest<UpdateFileMediaSystemRequest, UpdateFileMediaSystemResponse>(body, $"mediatransfer/start", HttpMethod.Post);
    }
    public async Task<UpdateFileMediaSystemResponse> DeleteFile(string fileName)
    {
        var body = new UpdateFileMediaSystemRequest
        {
            FileName = fileName,
        };

        return await FullSailRequest<UpdateFileMediaSystemRequest, UpdateFileMediaSystemResponse>(body, $"media/delete", HttpMethod.Delete);
    }
    public async Task<UpdateFileMediaSystemResponse> GetRunningJobs()
    {
        return await FullSailRequest<UpdateFileMediaSystemResponse>($"mediatransfer/listjobs", HttpMethod.Get);
    }
    public async Task<QBTResponse> GetRunningTorrents()
    {
        return await FullSailRequest<QBTResponse>($"torrents/list", HttpMethod.Get);
    }
    public async Task<UpdateTorrentsResponse> PauseTorrent(string hash)
    {
        var body = new UpdateTorrentsRequest
        {
            Hashes = new() { hash },
        };

        return await FullSailRequest<UpdateTorrentsRequest, UpdateTorrentsResponse>(body, $"torrents/pause", HttpMethod.Post);
    }
    public async Task<UpdateTorrentsResponse> ResumeTorrent(string hash)
    {
        var body = new UpdateTorrentsRequest
        {
            Hashes = new() { hash },
        };

        return await FullSailRequest<UpdateTorrentsRequest, UpdateTorrentsResponse>(body, $"torrents/resume", HttpMethod.Post);
    }
    public async Task<UpdateTorrentsResponse> DeleteTorrent(string hash)
    {
        var body = new UpdateTorrentsRequest
        {
            Hashes = new() { hash },
        };

        return await FullSailRequest<UpdateTorrentsRequest, UpdateTorrentsResponse>(body, $"torrents/delete", HttpMethod.Post);
    }
    public async Task<HealthCheckResponse> HealthCheck()
    {
        return await FullSailRequest<HealthCheckResponse>("healthcheck", HttpMethod.Get);
    }
    public FullSailClient UpdateSettings(string hostname, int port, string apiKey)
    {
        _hostName = hostname;
        _port = port;
        _apiKey = apiKey;
        return this;
    }
}
