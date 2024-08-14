using Microsoft.Playwright;

namespace PlaywrightXUnit.Config;

public class BrowserSettings
{
    public string[] Args { get; set; }
    public float? Timeout { get; set; }
    public bool Headless { get; set; }
    public int SlowMo { get; set; } 
    public DriverType DriverType { get; set; }
    public string Locale { get; set; }
    public string TimezoneId { get; set; } 
    public ColorScheme ColorScheme { get; set; }
    public string Url { get; set; } 
}

public enum DriverType
{
    Chromium,
    Firefox,
    WebKit,
    Chrome,
    Edge
}