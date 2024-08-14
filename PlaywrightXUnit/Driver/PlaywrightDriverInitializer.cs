using Microsoft.Playwright;
using PlaywrightXUnit.Config;

namespace PlaywrightXUnit.Driver;

public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
{
    public const float DEFAULT_TIMEOUT = 30f;

    public async Task<IBrowser> GetChromiumDriverAsync(BrowserSettings browserSettings)
    {
        var options = GetParameters(browserSettings.Args, browserSettings.Timeout, browserSettings.Headless, browserSettings.SlowMo);
        options.Channel = "chromium";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }

    public async Task<IBrowser> GetFireFoxDriverAsync(BrowserSettings browserSettings)
    {
        var options = GetParameters(browserSettings.Args, browserSettings.Timeout, browserSettings.Headless, browserSettings.SlowMo);
        return await GetBrowserAsync(DriverType.Firefox, options);
    }

    public async Task<IBrowser> GetWebKitDriverAsync(BrowserSettings browserSettings)
    {
        var options = GetParameters(browserSettings.Args, browserSettings.Timeout, browserSettings.Headless, browserSettings.SlowMo);
        return await GetBrowserAsync(DriverType.WebKit, options);
    }

    public async Task<IBrowser> GetChromeDriverAsync(BrowserSettings browserSettings)
    {
        var options = GetParameters(browserSettings.Args, browserSettings.Timeout, browserSettings.Headless, browserSettings.SlowMo);
        options.Channel = "chrome";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }

    public async Task<IBrowser> GetEdgeDriverAsync(BrowserSettings browserSettings)
    {
        var options = GetParameters(browserSettings.Args, browserSettings.Timeout, browserSettings.Headless, browserSettings.SlowMo);
        options.Channel = "msedge";
        return await GetBrowserAsync(DriverType.Chromium, options);
    }

    private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
    {
        var playwright = await Playwright.CreateAsync();

        return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);
    }

    private BrowserTypeLaunchOptions GetParameters(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = null)
    {
        return new BrowserTypeLaunchOptions
        {
            Args = args,
            Timeout = timeout,
            Headless = headless,
            SlowMo = slowmo,
        };
    }
}
