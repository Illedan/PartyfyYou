using Nancy;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class AuthorizationModule : NancyModule
    {
        public AuthorizationModule(IAuthorization authorization)
        {
            Get["/token"] = (parameters) =>
            {
                string code = this.Request.Query["code"];

                var tokenResponse = authorization.GetToken(code);

                return tokenResponse;
            };

            Get["/refreshToken"] = (parameters) =>
            {
                string refreshToken = this.Request.Query["refreshToken"];

                var tokenResponse = authorization.RefreshToken(refreshToken);

                return tokenResponse;
            };
        }
    }
}
