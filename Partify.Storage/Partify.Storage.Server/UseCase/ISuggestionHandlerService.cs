using Partify.Storage.Server.Suggestion;
using Partify.Storage.Server.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.UseCase
{
    public interface ISuggestionHandlerService
    {
        Task<SuggestionRelationResult> GetSuggestion(SuggestionRelationRequest suggestion);
        Task AddSuggestion(string videoId, string songId, Guid modeId, Guid userId);
        Task RemoveSuggestion(string videoId, string songId, Guid modeId, Guid userId);
        Task ReplaceSuggestion(string newVideoId, string songId, Guid modeId, Guid userId);
    }
}
