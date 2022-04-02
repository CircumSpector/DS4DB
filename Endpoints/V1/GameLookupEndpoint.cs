using System.Text.Json;
using DS4DB.Models.V1;
using FastEndpoints;
using MyCouch;

namespace DS4DB.Endpoints.V1;

/// <summary>
///     This endpoint lets users query for game entries by (partial) hash.
/// </summary>
public class GameLookupEndpoint : Endpoint<GameLookupRequest, List<GameEntry>>
{
    private readonly MyCouchStore _couchStore;

    private readonly ILogger<GameLookupEndpoint> _logger;

    public GameLookupEndpoint(ILogger<GameLookupEndpoint> logger, MyCouchStore couchStore)
    {
        _logger = logger;
        _couchStore = couchStore;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/games/{hash}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GameLookupRequest req, CancellationToken ct)
    {
        var query = new Query("gameHash", "game-hash").Configure(query => query
            .StartKey(req.Hash)
            .IncludeDocs(true)
            .Limit(50)
            .Reduce(false));

        var result = await _couchStore.QueryAsync<string>(query, ct);

        var entries = result.Select(row => JsonSerializer.Deserialize<GameEntry>(row.IncludedDoc,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }))
            .Where(entry => entry is not null)
            .Cast<GameEntry>()
            .ToList();

        if (!entries.Any())
        {
            _logger.LogInformation("Search for {Hash} yielded no results", req.Hash);
            await SendNotFoundAsync(ct);
            return;
        }

        await SendAsync(entries, cancellation: ct);
    }
}