using System.Text.Json.Serialization;

namespace DS4DB.Models.V1;

public class MainExecutable
{
    public string? Name { get; set; }

    public string? Version { get; set; }
}

public class Steam
{
    public string? AppId { get; set; }

    public string? StoreLink { get; set; }
}

public class NativeSupport
{
    public bool DualShock4 { get; set; }

    public bool Xbox360 { get; set; }
}

public class ControllerCompatibility
{
    public NativeSupport NativeSupport { get; set; } = new();
}

public class GameEntry
{
    [JsonPropertyName("_id")]
    public string Id { get; set; }

    [JsonPropertyName("_rev")]
    public string Rev { get; set; }

    public string Sha256 { get; set; }

    public string? Name { get; set; }

    public MainExecutable MainExecutable { get; set; } = new();

    public Steam Steam { get; set; } = new();

    public ControllerCompatibility ControllerCompatibility { get; set; } = new();
}