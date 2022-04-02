using DS4DB.Models.V1;
using FastEndpoints;
using MyCouch;

namespace DS4DB.Endpoints.V1;

/// <summary>
///     This endpoint allows game entry inserts.
/// </summary>
public class GameInsertEndpoint : Endpoint<GameEntry, GameInsertResponse>
{
    private readonly MyCouchStore _couchStore;

    public GameInsertEndpoint(MyCouchStore couchStore)
    {
        _couchStore = couchStore;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/v1/games");
    }

    public override async Task HandleAsync(GameEntry req, CancellationToken ct)
    {
        var entry = await _couchStore.StoreAsync(req, ct);

        await SendOkAsync(new GameInsertResponse { Id = entry.Id }, ct);
    }
}