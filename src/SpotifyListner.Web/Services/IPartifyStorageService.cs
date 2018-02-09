using System;
using System.Threading.Tasks;
using SpotifyListner.Web.Models;

namespace SpotifyListner.Web.Services
{
    public interface IPartifyStorageService
    {
        Task AddSuggestion(string videoId, string songId, Guid modeId, Guid userId);
        Task<SuggestionRelationResult> GetSuggestion(string songId, Guid modeId, Guid userId);
    }
}