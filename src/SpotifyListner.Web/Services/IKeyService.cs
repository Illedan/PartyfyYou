using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public interface IKeyService
    {
        Task<KeyModel> GetKeys();
    }
}