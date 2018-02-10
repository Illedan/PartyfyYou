using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public interface ISpotifyService
    {
        Task PauseSong(string videoId, string token);

        Task<SpotifyContent> GetCurrentSong(string token);
        Task<SpotifyUser> GetUser(string token);
    }
}