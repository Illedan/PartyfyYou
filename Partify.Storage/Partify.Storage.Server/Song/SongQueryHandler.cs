using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace Partify.Storage.Server.Song
{
    public class SongQueryHandler : IQueryHandler<SongQuery, SongResult>
    {
        private readonly IDbConnection m_dbConnection;

        public SongQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task<SongResult> HandleAsync(SongQuery query)
        {
            var result = await m_dbConnection.QueryAsync<SongResult>(Sql.SongBySpotifyId, query);
            return result.FirstOrDefault();
        }
    }
}
