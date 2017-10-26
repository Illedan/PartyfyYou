using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public interface IAuthorization
    {
        TokenResponse GetToken(string code);

        RefreshTokenResponse RefreshToken(string refreshToken);
    }
}
