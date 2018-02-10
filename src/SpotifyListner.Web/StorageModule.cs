using System;
using Nancy;
using SpotifyListner.Web.Models;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class StorageModule : NancyModule
    {
        public StorageModule(IPartifyStorageService partifyStorageService)
        {
            Get["/store", true] = async (parameters, ct) =>
            {
                string userId = this.Request.Query["userId"];
                string modeId = this.Request.Query["modeId"];
                string videoId = this.Request.Query["videoId"];
                string songId = this.Request.Query["songId"];
                if (!string.IsNullOrEmpty(songId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(modeId) && !string.IsNullOrEmpty(videoId))
                {
                    var userIdDidParse = Guid.TryParse(userId, out var userIdGuid);
                    var modeIdDidParse = Guid.TryParse(modeId, out var modeIdGuid);

                    await partifyStorageService.AddSuggestion(videoId, songId, modeIdGuid, userIdGuid);

                    return true;

                }

                return false;

            };
           
        }
    }
}