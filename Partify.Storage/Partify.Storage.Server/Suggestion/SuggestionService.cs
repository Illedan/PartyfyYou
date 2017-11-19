using Partify.Storage.Server.CQRS;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionService : ISuggestionService
    {
        private readonly ICommandExecutor m_commandExecutor;
        private readonly IQueryExecutor m_queryExecutor;
        public SuggestionService(ICommandExecutor commandExecutor, IQueryExecutor queryExecutor)
        {
            m_commandExecutor = commandExecutor;
            m_queryExecutor = queryExecutor;
        }

        public async Task PostSuggestion(CreateSuggestionRequest request)
        {
            await m_commandExecutor.ExecuteAsync(
                new SuggestionCommand {
                    Id = Guid.NewGuid(),
                    Count = request.Count,
                    ModeId = request.ModeId,
                    Overruled = request.Overruled,
                    SpotifyId = request.SpotifyId,
                    YoutubeId = request.YoutubeId }
                );
        }

        public async Task<SuggestionResult> GetSuggestionRelation(string videoId, string songId, string modeName, string userName)
        {
            var result = await m_queryExecutor.ExecuteAsync(
                new SuggestionQuery {
                    VideoId = videoId,
                    SongId = songId,
                    ModeName = modeName,
                    UserName = userName
                });

            return result.FirstOrDefault();
        }
    }
}
