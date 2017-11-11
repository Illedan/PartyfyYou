using Partify.Storage.Query;
using System;

namespace Partify.Storage.Video
{
    public class VideoQuery : IQuery<VideoResult>
    {
        public Guid Id { get; set; }
    }
}
