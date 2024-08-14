using Microsoft.Playwright;
using PlaywrightXUnit.Config;

namespace PlaywrightXUnit.Driver;
public interface IPlaywrightDriverInitializer
{
    Task<IBrowser> GetChromiumDriverAsync(BrowserSettings browserSettings);
    Task<IBrowser> GetFireFoxDriverAsync(BrowserSettings browserSettings);
    Task<IBrowser> GetWebKitDriverAsync(BrowserSettings browserSettings);
    Task<IBrowser> GetChromeDriverAsync(BrowserSettings browserSettings);
    Task<IBrowser> GetEdgeDriverAsync(BrowserSettings browserSettings);
}