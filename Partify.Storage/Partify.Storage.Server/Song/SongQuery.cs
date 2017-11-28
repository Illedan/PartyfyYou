using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Song
{
    public class SongQuery : IQuery<SongResult>
    {
        public string SongId { get; set; }
    }
}
