using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services.WebApi
{
    public interface ISpotifyWebApi
    {
        Task<string> RefreshToken(string token);
        Task<SpotifyContent> GetPlayingSong(string token);
    }
}