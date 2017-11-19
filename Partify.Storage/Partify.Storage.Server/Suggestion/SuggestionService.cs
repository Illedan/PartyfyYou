using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionService : ISuggestionService
    {
        private readonly ICommandExecutor m_commandExecutor;
        public SuggestionService(ICommandExecutor commandExecutor)
        {
            m_commandExecutor = commandExecutor;
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
    }
}
