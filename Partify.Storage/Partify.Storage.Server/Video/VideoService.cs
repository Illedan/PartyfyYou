using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Video
{
    public class VideoService : IVideoService
    {
        private readonly ICommandExecutor m_commandExecutor;
        private readonly IQueryExecutor m_queryExecutor;
        public VideoService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            m_commandExecutor = commandExecutor;
            m_queryExecutor = queryExecutor;
        }

        public async Task<VideoResult> GetVideo(string videoId)
        {
            var result = await m_queryExecutor.ExecuteAsync(new VideoQuery { VideoId = videoId});
            return result;
        }

        public async Task PostVideo(CreateVideoRequest request)
        {
            await m_commandExecutor.ExecuteAsync(new VideoCommand { Id = Guid.NewGuid(), VideoId = request.VideoId});
        }
    }
}
