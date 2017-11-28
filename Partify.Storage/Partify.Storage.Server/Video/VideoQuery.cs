using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Video
{
    public class VideoQuery : IQuery<VideoResult>
    {
        public string VideoId { get; set; }
    }
}
