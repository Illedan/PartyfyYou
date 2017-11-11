using Partify.Storage.Query;
using System;

namespace Partify.Storage.Song
{
    public class SongQuery : IQuery<SongResult>
    {
        public Guid Id { get; set; }
    }
}
