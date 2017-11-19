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
    public class CreateSongCommandHandler : ICommandHandler<CreateSongCommand>
    {
        private readonly IDbConnection m_dbConnection;

        public CreateSongCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task HandleAsync(CreateSongCommand command)
        {
            await m_dbConnection.QueryAsync(Sql.PostSong, command);
        }
    }
}
