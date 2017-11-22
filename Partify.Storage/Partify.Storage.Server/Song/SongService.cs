using Partify.Storage.Server.CQRS;
using Partify.Storage.Server.Song;
using System;
using System.Threading.Tasks;

namespace Partify.Storage.Server.SpotifySong
{
    public class SongService : ISongService
    {
        private readonly IQueryExecutor m_queryExecutor;
        private readonly ICommandExecutor m_commandExecutor;

        public SongService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task<SongResult> Get(string spotifySongId)
        {
            var result = await m_queryExecutor.ExecuteAsync(new SongQuery { SongId = spotifySongId});
            return result;
        }

        public async Task Post(CreateSongRequest songResult)
        {
            var createSongCommand = new SongCommand
            {
                Id = Guid.NewGuid(),
                SongId = songResult.SongId
            };

            await m_commandExecutor.ExecuteAsync(createSongCommand);
        }
    }
}
