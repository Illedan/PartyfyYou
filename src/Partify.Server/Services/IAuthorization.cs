using System.Threading.Tasks;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public interface IAuthorization
    {
        Task<TokenResponse> GetToken(string code);

        Task<RefreshTokenResponse> RefreshToken(string refreshToken);
    }
}
