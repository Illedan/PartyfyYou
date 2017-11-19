using System;

namespace Partify.Storage.Server.SpotifySong
{
    internal class CreateSongCommand
    {
        public Guid Id { get; set; }
        public string SongId { get; set; }
    }
}