using Nancy;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class AuthorizationModule : NancyModule
    {
        public AuthorizationModule(IAuthorization authorization)
        {
            Get["/token", true] = async (parameters, ct) =>
            {
                string code = this.Request.Query["code"];

                var tokenResponse = await authorization.GetToken(code);

                return tokenResponse;
            };

            Get["/refreshToken", true] = async (parameters, ct) =>
            {
                string refreshToken = this.Request.Query["refreshToken"];

                var tokenResponse = await authorization.RefreshToken(refreshToken);

                return tokenResponse;
            };
        }
    }
}
