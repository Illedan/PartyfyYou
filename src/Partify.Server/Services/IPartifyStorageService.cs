using System;
using System.Threading.Tasks;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public interface IPartifyStorageService
    {
        Task AddSuggestion(string videoId, string songId, Guid modeId, Guid userId);
        Task<SuggestionRelationResult> GetSuggestion(string songId, Guid modeId, Guid userId);
    }
}