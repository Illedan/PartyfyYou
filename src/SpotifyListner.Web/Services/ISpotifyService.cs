using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public interface ISpotifyService
    {
        Task<SpotifySong> GetCurrentSong();

        Task PauseSong(string videoId);
    }
}