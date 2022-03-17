using System.Text.Json.Serialization;

namespace DS4DB.Models.V1;

public class MainExecutable
{
    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("version")] public string? Version { get; set; }
}

public class Steam
{
    [JsonPropertyName("appId")] public string? AppId { get; set; }

    [JsonPropertyName("storeLink")] public string? StoreLink { get; set; }
}

public class NativeSupport
{
    [JsonPropertyName("dualShock4")] public bool DualShock4 { get; set; }

    [JsonPropertyName("xbox360")] public bool Xbox360 { get; set; }
}

public class ControllerCompatibility
{
    [JsonPropertyName("nativeSupport")] public NativeSupport NativeSupport { get; set; } = new();
}

public class GameResponse
{
    [JsonPropertyName("sha256")] public string Sha256 { get; set; }

    [JsonPropertyName("name")] public string? Name { get; set; }

    [JsonPropertyName("mainExecutable")] public MainExecutable MainExecutable { get; set; } = new();

    [JsonPropertyName("steam")] public Steam Steam { get; set; } = new();

    [JsonPropertyName("controllerCompatibility")]
    public ControllerCompatibility ControllerCompatibility { get; set; } = new();
}