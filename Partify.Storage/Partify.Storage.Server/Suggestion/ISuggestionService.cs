using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Suggestion
{
    public interface ISuggestionService
    {
        Task PostSuggestion(CreateSuggestionRequest request);
        Task<SuggestionRelationResult> GetSuggestionRelation(SuggestionRelationRequest suggestion);
        Task<SuggestionResult> GetSuggestion(Guid songId, Guid videoId, Guid modeId);
        Task DeleteSuggestion(Guid suggestionId);
    }
}
