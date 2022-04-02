namespace DS4DB.Auth;

public class SimpleUserAuthService : IUserService
{
    private readonly ServiceConfig _config;

    public SimpleUserAuthService(ServiceConfig config)
    {
        _config = config;
    }

    public Task<User> Authenticate(string username, string password)
    {
        if (!_config.Credentials.Any(credential =>
                Equals(credential.Username, username) && Equals(credential.Password, password)))
            throw new UnauthorizedAccessException();

        return Task.FromResult(new User { Id = username, Username = username });
    }
}