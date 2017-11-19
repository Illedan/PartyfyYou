namespace Partify.Storage.Server.User
{
    public class CreateUserRequest
    {
        public string SpotifyUserId { get; internal set; }
        public string Name { get; internal set; }
        public string Country { get; internal set; }
    }
}