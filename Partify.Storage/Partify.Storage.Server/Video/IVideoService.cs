using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Video
{
    public interface IVideoService
    {
        Task PostVideo(CreateVideoRequest request);
    }
}
