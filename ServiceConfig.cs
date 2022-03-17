using MyCouch;

namespace DS4DB;

public class CouchDbConfig
{
    public string Uri { get; set; }

    public string Database { get; set; }
    
    public DbConnectionInfo ConnectionInfo => new(Uri, Database);
}

public class ServiceConfig
{
    public CouchDbConfig CouchDb { get; set; } = new();
}