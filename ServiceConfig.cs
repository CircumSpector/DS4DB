using MyCouch;

namespace DS4DB;

public class CouchDbConfig
{
    public string Uri { get; set; }

    public string Database { get; set; }
    
    public DbConnectionInfo ConnectionInfo => new(Uri, Database);
}

public class Credential
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class ServiceConfig
{
    public CouchDbConfig CouchDb { get; set; } = new();

    public List<Credential> Credentials { get; set; } = new();
}