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

        public async Task<SuggestionRelationResult> GetSuggestionRelation(SuggestionRelationRequest suggestion)
        {
            var result = await m_queryExecutor.ExecuteAsync(
                new SuggestionRelationQuery {
                    SongId = suggestion.SongId,
                    ModeId = suggestion.ModeId,
                    UserId = suggestion.UserId
                });

            return result.FirstOrDefault();
        }

        public async Task<SuggestionResult> GetSuggestion(Guid songId, Guid videoId, Guid modeId)
        {
            var result = await m_queryExecutor.ExecuteAsync(new SuggestionQuery {ModeId = modeId, SpotifyId = songId, YoutubeId = videoId });
            return result;
        }
    }
}
