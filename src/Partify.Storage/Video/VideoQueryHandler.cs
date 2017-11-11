using Partify.Storage.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Video
{
    public class VideoQueryHandler : IQueryHandler<VideoQuery, VideoResult>
    {
        public Task<VideoResult> HandleAsync(VideoQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
