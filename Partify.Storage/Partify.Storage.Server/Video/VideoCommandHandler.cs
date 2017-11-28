using Partify.Storage.Server.CQRS;
using System;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Video
{
    public class VideoCommandHandler : ICommandHandler<VideoCommand>
    {
        private readonly IDbConnection m_dbConnection;
        public VideoCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task HandleAsync(VideoCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostVideo, command);
        }
    }
}
