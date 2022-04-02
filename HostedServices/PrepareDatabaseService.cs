using MyCouch;

namespace DS4DB.HostedServices;

/// <summary>
///     Performs database initialization tasks.
/// </summary>
public class PrepareDatabaseService : IHostedService
{
    private readonly MyCouchStore _couchStore;

    public PrepareDatabaseService(MyCouchStore couchStore)
    {
        _couchStore = couchStore;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        // Put the view we need to query into the DB, if absent for whatever reason
        if (!await _couchStore.ExistsAsync(@"_design/gameHash", cancellationToken: cancellationToken))
            await _couchStore.Client.Documents.PostAsync(@"{
""_id"": ""_design/gameHash"",
""views"": {
    ""game-hash"": {
      ""map"": ""function (doc) {\n  emit(doc.sha256);\n}""
    }
  },
  ""language"": ""javascript""
}", cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}