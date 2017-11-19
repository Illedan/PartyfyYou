using Partify.Storage.Server.Mode;
using System.Threading.Tasks;

namespace Partify.Storage.Server.SpotifySong
{
    public interface ISongService
    {
        Task Post(CreateSongRequest songResult);
    }
}