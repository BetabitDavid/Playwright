using Microsoft.Playwright;
using PlaywrightXUnit.Config;

namespace PlaywrightXUnit.Driver;

public class PlaywrightDriver : IDisposable
{
    private AsyncTask<IBrowserContext> _browserContext;
    private AsyncTask<IBrowser> _browser;
    private AsyncTask<IPage> _page;
    private readonly BrowserSettings _browserSettings;
    private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
    private bool _isDisposed;

    public PlaywrightDriver(BrowserSettings browserSettings, IPlaywrightDriverInitializer playwrightDriverInitializer)
    {
        _browserSettings = browserSettings;
        _playwrightDriverInitializer = playwrightDriverInitializer;

        _browser = new AsyncTask<IBrowser>(InitializePlaywrightDriver);
        _browserContext = new AsyncTask<IBrowserContext>(GetBrowserContextAsync);
        _page = new AsyncTask<IPage>(GetPageAsync);
    }

    public Task<IPage> Page => _page.Value;
    public Task<IBrowser> Browser => _browser.Value;
    public Task<IBrowserContext> BrowserContext => _browserContext.Value;

    private async Task<IBrowser> InitializePlaywrightDriver()
    {
        return _browserSettings.DriverType switch
        {
            DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_browserSettings),
            DriverType.Firefox => await _playwrightDriverInitializer.GetFireFoxDriverAsync(_browserSettings),
            DriverType.WebKit => await _playwrightDriverInitializer.GetWebKitDriverAsync(_browserSettings),
            DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_browserSettings),
            DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_browserSettings),
            _ => await _playwrightDriverInitializer.GetEdgeDriverAsync(_browserSettings)
        };
    }

    private async Task<IPage> GetPageAsync()
    {
        var page = await (await _browserContext).NewPageAsync();
        return page;
    }

    private async Task<IBrowserContext> GetBrowserContextAsync()
    {
        return await (await _browser).NewContextAsync(new()
        {
            Locale = _browserSettings.Locale,
            TimezoneId = _browserSettings.TimezoneId,
            ColorScheme = _browserSettings.ColorScheme,
        });
    }

    public void Dispose()
    {
        if (_isDisposed)
        {
            return;
        }
        if (_browser.IsValueCreated)
        {
            Task.Run(async () =>
            {
                await (await _browser).CloseAsync();
                await (await _browser).DisposeAsync();
            });
        }
        _isDisposed = true;
    }
}