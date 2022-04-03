using System.Text.Json.Serialization;

namespace DS4DB.Models.V1;

/// <summary>
///     Represents a document per game version with various compatibility information.
/// </summary>
/// <remarks>Work in progress!</remarks>
public class GameEntry
{
    /// <summary>
    ///     The internal database unique ID.
    /// </summary>
    [JsonPropertyName("_id")] public string Id { get; set; }

    /// <summary>
    ///     The internal database revision ID.
    /// </summary>
    [JsonPropertyName("_rev")] public string Rev { get; set; }

    /// <summary>
    ///     SHA256 hash of the <see cref="MainExecutable"/>. This uniquely identifies each record.
    /// </summary>
    public string Sha256 { get; set; } = null!;

    /// <summary>
    ///     Display/friendly-name of the game.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Main executable details.
    /// </summary>
    public MainExecutable MainExecutable { get; set; } = new();

    /// <summary>
    ///     Steam-specific properties.
    /// </summary>
    public Steam? Steam { get; set; }

    public ControllerCompatibility ControllerCompatibility { get; set; } = new();
}

/// <summary>
///     Represents the main module that launches the game process.
/// </summary>
public class MainExecutable
{
    /// <summary>
    ///     The executable name with extension and without path.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    ///     The product- or file-version string from the executable metadata.
    /// </summary>
    public string Version { get; set; } = null!;
}

/// <summary>
///     Steam-specific properties (only provided if the game is available through Steam Store).
/// </summary>
public class Steam
{
    /// <summary>
    ///     The unique application ID of the game in the Steam universe.
    /// </summary>
    public ulong AppId { get; set; }

    /// <summary>
    ///     Absolute URL to Steam Store Page.
    /// </summary>
    public Uri? StoreLink { get; set; }
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