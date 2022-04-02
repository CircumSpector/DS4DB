namespace DS4DB.Auth;

public interface IUserService
{
    Task<User> Authenticate(string username, string password);
}