using PlaywrightXUnit.Config;
using PlaywrightXUnit.Driver;

namespace PlaywrightTests.Tests;

public class HLTVTests : IClassFixture<PlaywrightDriverInitializer>
{
    private PlaywrightDriver _playwrightDriver;
    private BrowserSettings _browserSettings;

    public HLTVTests(PlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _browserSettings = ConfigReader.ReadConfig();
        _playwrightDriver = new PlaywrightDriver(_browserSettings, playwrightDriverInitializer);
    }

    [Fact]
    public async Task CheckFantasyPage()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_browserSettings.Url);
        await page.ClickAsync("text=Allow all cookies");
        await page.ClickAsync("text=Fantasy");
    }
    
    [Fact]
    public async Task CheckTeamRanking()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_browserSettings.Url);
        await page.ClickAsync("text=Allow all cookies");
        await page.ClickAsync("text=Complete ranking");
    }

}
