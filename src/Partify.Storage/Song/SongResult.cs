using System;

namespace Partify.Storage.Song
{
    public class SongResult
    {
        public Guid Id { get; set; }
        public string SpotifyId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string AlbumId { get; set; }
    }
}
