using Partify.Storage.Server.CQRS;
using System.Linq;
using System.Data;
using Dapper;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Video
{
    public class VideoQueryHandler : IQueryHandler<VideoQuery, VideoResult>
    {
        private readonly IDbConnection m_dbConnection;

        public VideoQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task<VideoResult> HandleAsync(VideoQuery query)
        {
            var result = await m_dbConnection.QueryAsync(Sql.VideoByVideoId,query);
            return result.FirstOrDefault();
        }
    }
}
