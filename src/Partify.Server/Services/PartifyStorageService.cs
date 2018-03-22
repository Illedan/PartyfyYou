using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Partify.Server.Models;

namespace Partify.Server.Services
{
    public class PartifyStorageService: IPartifyStorageService
    {
        private readonly IRestClientWrapper _restClientWrapper;

        public PartifyStorageService(IRestClientWrapper restClientWrapper)
        {
            _restClientWrapper = restClientWrapper;
        }
        public async Task AddSuggestion(string videoId, string songId, Guid modeId, Guid userId)
        {
            var psm = new PostSuggestionModel {ModeId = modeId, SongId = songId, UserId = userId, VideoId = videoId};
            var res= await _restClientWrapper.PostAsync_HttpResponse(psm, "Suggestion");
        }

        public async Task<SuggestionRelationResult> GetSuggestion(string songId, Guid modeId, Guid userId)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"songId", songId},
                {"modeId", modeId},
                {"userId", userId}
            };
            var result = await _restClientWrapper.GetAsync<SuggestionRelationResult>(dictionary, "Suggestion");
            return result;
        }
    }
}