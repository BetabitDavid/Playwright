using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlaywrightXUnit.Config;

public static class ConfigReader
{
    public static BrowserSettings ReadConfig()
    {
        var configFile = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/appsettings.json");
        var jsonSerializerSettings = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        jsonSerializerSettings.Converters.Add(new JsonStringEnumConverter());

        var settings = JsonSerializer.Deserialize<BrowserSettings>(configFile, jsonSerializerSettings);

        return settings ?? throw new Exception("Config file is empty or invalid");
    }
}
