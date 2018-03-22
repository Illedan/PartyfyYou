using System.Threading.Tasks;
using Partify.Server.Models;
namespace Partify.Server.Services.WebApi
{
    public interface ISpotifyWebApi
    {
        Task<string> RefreshToken(string token);
        Task<SpotifyContent> GetPlayingSong(string token);
        Task<SpotifyUser> GetUser(string token);
    }
}