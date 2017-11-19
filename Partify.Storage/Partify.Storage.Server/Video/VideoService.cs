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
        public VideoService(ICommandExecutor commandExecutor)
        {
            m_commandExecutor = commandExecutor;
        }
        public async Task PostVideo(CreateVideoRequest request)
        {
            await m_commandExecutor.ExecuteAsync(new VideoCommand { Id = Guid.NewGuid(), VideoId = request.VideoId});
        }
    }
}
