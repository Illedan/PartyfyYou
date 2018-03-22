using System.Threading.Tasks;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public interface ISpotifyService
    {
        Task PauseSong(string videoId, string token);

        Task<SpotifyContent> GetCurrentSong(string token);
        Task<SpotifyUser> GetUser(string token);
    }
}