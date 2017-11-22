using Dapper;
using Partify.Storage.Server.CQRS;
using Partify.Storage.Server.SpotifySong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Song
{
    public class SongCommandHandler : ICommandHandler<SongCommand>
    {
        private readonly IDbConnection m_dbConnection;

        public SongCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task HandleAsync(SongCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostSong, command);
        }
    }
}
