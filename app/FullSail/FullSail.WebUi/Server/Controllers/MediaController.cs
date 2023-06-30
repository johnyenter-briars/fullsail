using FullSail.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FullSail.WebUi.Server.Controllers;

[Route("[controller]")]
public class MediaController : ControllerBase
{
    private readonly ILogger<MediaController> _logger;
    private readonly KodiClient _kodiClient;

    public MediaController(ILogger<MediaController> logger, KodiClient kodiClient)
    {
        _logger = logger;
        _kodiClient = kodiClient;
    }
    [HttpPost("down")]
    public async Task Down()
    {
        await _kodiClient.InputDownAsync();
    }
    [HttpPost("up")]
    public async Task Up()
    {
        await _kodiClient.InputUpAsync();
    }
    [HttpPost("osd")]
    public async Task Osd()
    {
        await _kodiClient.ShowOSD();
    }
    [HttpPost("left")]
    public async Task Left()
    {
        await _kodiClient.InputLeftAsync();
    }
    [HttpPost("enter")]
    public async Task Enter()
    {
        await _kodiClient.InputSelectAsync();
    }
    [HttpPost("right")]
    public async Task Right()
    {
        await _kodiClient.InputRightAsync();
    }
    [HttpPost("escape")]
    public async Task Escape()
    {
        await _kodiClient.InputBackAsync();
    }
    [HttpPost("toggleplaypause")]
    public async Task TogglePlayPause()
    {
        await _kodiClient.TogglePlayPausePlayerAsync();
    }
    [HttpPost("reboot")]
    public async Task Reboot()
    {
        await _kodiClient.PowerReboot();
    }
    [HttpPost("poweroff")]
    public async Task PowerOff()
    {
        await _kodiClient.PowerOff();
    }
    [HttpPost("rewind")]
    public async Task Rewind([FromQuery] int speed)
    {
        await _kodiClient.SetPlayerSpeed(speed);
    }
    [HttpPost("fastforward")]
    public async Task FastForward([FromQuery] int speed)
    {
        await _kodiClient.SetPlayerSpeed(speed);
    }
    [HttpPost("sendtext")]
    public async Task SendText()
    {
        await _kodiClient.InputSendText("test");
    }
    [HttpPost("sendtextchanged")]
    public async Task SendTextChanged()
    {
        await _kodiClient.InputText("test");
    }
    [HttpPost("volume")]
    public async Task Volume([FromQuery] int percentVolume)
    {
        await _kodiClient.SetVolumeAsync(percentVolume);
    }
    [HttpPost("mute")]
    public async Task Mute()
    {
        await _kodiClient.SetVolumeAsync(0);
    }
    [HttpPost("kodisettings")]
    public async Task KodiSettings([FromQuery] string hostname, [FromQuery] int port, [FromQuery] string username, [FromQuery] string password)
    {
        _kodiClient.UpdateSettings(hostname, port, username, password);
    }
    [HttpPost("seek")]
    public async Task Seek([FromQuery] int percentage)
    {
        await _kodiClient.SeekPlayerAsync(percentage);
    }
}