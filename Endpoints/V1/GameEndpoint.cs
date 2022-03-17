using System.Text.Json;
using DS4DB.Models.V1;
using FastEndpoints;
using MyCouch;

namespace DS4DB.Endpoints.V1;

public class GameEndpoint : Endpoint<GameRequest, List<GameResponse>>
{
    private readonly MyCouchStore _couchStore;

    private readonly ILogger<GameEndpoint> _logger;

    public GameEndpoint(ServiceConfig config, ILogger<GameEndpoint> logger)
    {
        _logger = logger;
        _couchStore = new MyCouchStore(config.CouchDb.Uri, config.CouchDb.Database);
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/v1/games/{hash}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GameRequest req, CancellationToken ct)
    {
        var query = new Query("gameHash", "game-hash").Configure(query => query
            .StartKey(req.Hash)
            .IncludeDocs(true)
            .Limit(10)
            .Reduce(false));

        var result = await _couchStore.QueryAsync<string>(query, ct);
        
        var entries = result.Select(row => JsonSerializer.Deserialize<GameResponse>(row.IncludedDoc)).ToList();
        
        await SendAsync(entries, cancellation: ct);
    }
}