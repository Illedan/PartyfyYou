using Nancy;
using SpotifyListner.Web.Models;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class UserModule : NancyModule
    {
        public UserModule(IRestClientWrapper restClientWrapper, ISpotifyService spotifyService)
        {
            Get["/user", true] = async (parameters, ct) =>
            {
                string name = this.Request.Query["Name"];
                string contry = this.Request.Query["Contry"];
                string spotifyUserId = this.Request.Query["SpotifyUserId"];
                
                var user = new User { Country = contry, Name = name, SpotifyUserId = spotifyUserId};

                var userResult = await restClientWrapper.PostAsync<UserResult>(user, "User");
                return userResult;
            };
            Get["/spotifyUser", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];

                var user = await spotifyService.GetUser(token);


                return user;
            };
        }
    }
}