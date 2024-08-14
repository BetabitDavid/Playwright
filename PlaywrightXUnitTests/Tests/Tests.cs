using PlaywrightXUnit.Config;
using PlaywrightXUnit.Driver;

namespace PlaywrightTests.Tests;

public class Tests : IClassFixture<PlaywrightDriverInitializer>
{
    private PlaywrightDriver _playwrightDriver;
    private BrowserSettings _browserSettings;

    public Tests(PlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _browserSettings = ConfigReader.ReadConfig();
        _playwrightDriver = new PlaywrightDriver(_browserSettings, playwrightDriverInitializer);
    }

    [Fact]
    public async Task OpenTextBox()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_browserSettings.Url);
        await page.ClickAsync("text=Elements");
        await page.ClickAsync("text=Text Box");
    }
    
    [Fact]
    public async Task OpenAccordian()
    {
        var page = await _playwrightDriver.Page;
        await page.GotoAsync(_browserSettings.Url);
        await page.ClickAsync("text=Widgets");
        await page.ClickAsync("text=Accordian");
    }
}
