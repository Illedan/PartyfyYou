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
        Task<TokenResponse> GetToken(string code);

        Task<RefreshTokenResponse> RefreshToken(string refreshToken);
    }
}
