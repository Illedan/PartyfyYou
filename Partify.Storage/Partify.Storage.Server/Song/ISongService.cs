using Partify.Storage.Server.Mode;
using Partify.Storage.Server.Song;
using System.Threading.Tasks;

namespace Partify.Storage.Server.SpotifySong
{
    public interface ISongService
    {
        Task Post(CreateSongRequest songResult);

        Task<SongResult> Get(string spotifySongId);
    }
}