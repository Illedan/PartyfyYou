using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public interface IYouTubeGoogleService
    {
        Task<string> FetchUrl(SpotifyContent spotifySong, string mode);

        Task<TimeSpan> GetSongLength(string id);
        Task<List<SpotifySearchResult>> GetSearchResults(SpotifyContent spotifySong, string mode, int maxResults);
    }
}