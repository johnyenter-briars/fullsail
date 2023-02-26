﻿using FullSail.Managers;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace FullSail;

class KodiClient
{
    private static readonly HttpClient client = new HttpClient();
    private async Task PostRequestAsync(string requestObject)
    {
        var username = PreferencesManager.GetKodiUsername();
        var password = PreferencesManager.GetKodiPassword();
        var hostname = PreferencesManager.GetKodiHostname();
        var port = PreferencesManager.GetKodiPort();

        var requestBody = new StringContent(requestObject);
        requestBody.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var credential = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{username}:{password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);

        var request = new HttpRequestMessage(HttpMethod.Post, $"http://{hostname}:{port}/jsonrpc");
        request.Content = requestBody;

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var jsonBody = await response.Content.ReadAsStringAsync();
    }

    public async Task PowerOff()
    {
        var requestObject = @"
            {
                ""jsonrpc"": ""2.0"",
                ""method"": ""System.Shutdown"",
                ""id"": 1
            }";

        await PostRequestAsync(requestObject);
    }

    public async Task ShowOSD()
    {
        var requestObject = @"
            {
                ""jsonrpc"": ""2.0"",
                ""method"": ""Input.ShowOSD"",
                ""id"": 1
            }";

        await PostRequestAsync(requestObject);
    }

    public async Task PowerSuspend()
    {
        var requestObject = @"
            {
                ""jsonrpc"": ""2.0"",
                ""method"": ""System.Suspend"",
                ""id"": 1
            }";

        await PostRequestAsync(requestObject);
    }

    public async Task PowerReboot()
    {
        var requestObject = @"
            {
                ""jsonrpc"": ""2.0"",
                ""method"": ""System.Reboot"",
                ""id"": 1
            }";

        await PostRequestAsync(requestObject);
    }

    public async Task SetPlayerSpeed(int speed)
    {
        var requestObject = $@"
            {{
                ""jsonrpc"": ""2.0"",
                ""id"": 1,
                ""method"": ""Player.SetSpeed"",
                ""params"": {{
                    ""playerid"": 1,
                    ""speed"": {speed}
                }}
            }}";

        await PostRequestAsync(requestObject);
    }

    public async Task InputSendText(string text)
    {
        var requestObject = $@"
            {{
                ""jsonrpc"": ""2.0"",
                ""id"": 1,
                ""method"": ""Input.SendText"",
                ""params"": {{
                    ""text"": ""{text}"",
                    ""done"": true
                }}
            }}";

        await PostRequestAsync(requestObject);
    }

    public async Task InputText(string text)
    {
        var requestObject = $@"
            {{
                ""jsonrpc"": ""2.0"",
                ""id"": 1,
                ""method"": ""Input.SendText"",
                ""params"": {{
                    ""text"": ""{text}"",
                    ""done"": false
                }}
            }}";

        await PostRequestAsync(requestObject);
    }
    async Task SeekPlayerAsync(int percentage)
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Player.Seek"",
        ""params"": {{
            ""playerid"": 1,
            ""value"": {{
                ""percentage"": {percentage}
            }}
        }}
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task SetVolumeAsync(int level)
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Application.SetVolume"",
        ""params"": {{
            ""volume"": {level}
        }}
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputSelectAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Select""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputBackAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Back""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputExecuteActionAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.ExecuteAction""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputLeftAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Left""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputRightAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Right""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputDownAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Down""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task InputUpAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Input.Up""
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task StopPlayerAsync()
    {
        var requestObject = $@"
    {{
        ""jsonrpc"": ""2.0"",
        ""id"": 1,
        ""method"": ""Player.Stop"",
        ""params"": {{
            ""playerid"": 1
        }}
    }}";
        await PostRequestAsync(requestObject);
    }

    async Task TogglePlayPausePlayerAsync()
    {
        var requestObject = $@"
        {{
            ""jsonrpc"": ""2.0"",
            ""id"": 1,
            ""method"": ""Player.PlayPause"",
            ""params"": {{
                ""playerid"": 1
            }}
        }}";

        await PostRequestAsync(requestObject);
    }
}
