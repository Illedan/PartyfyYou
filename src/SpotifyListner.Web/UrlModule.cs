using System;
using Nancy;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.Security;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        public UrlModule(IYouTubeGoogleService youTubeGoogleService, ISpotifyService spotifyService, IPartifyStorageService partifyStorageService)
        {
            Get["/url", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];
                string mode = this.Request.Query["mode"];
                string userId = this.Request.Query["userId"];
                string modeId = this.Request.Query["modeId"];
                var song = (await spotifyService.GetCurrentSong(token));
                var songId = song?.item?.id;
                string videoId = null;
                
                if (!string.IsNullOrEmpty(songId) && !string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(modeId))
                {
                    var userIdDidParse = Guid.TryParse(userId, out var userIdGuid);
                    var modeIdDidParse = Guid.TryParse(modeId, out var modeIdGuid);
                    var suggestion = await partifyStorageService.GetSuggestion(songId, modeIdGuid, userIdGuid);
                    if (suggestion!= null)
                    {
                        return suggestion.YoutubeVideoId;
                    }
                    videoId = await youTubeGoogleService.FetchUrl(song, mode);
                    if (!string.IsNullOrEmpty(videoId))
                    {
                        
                        await partifyStorageService.AddSuggestion(videoId, songId, modeIdGuid, userIdGuid);
                    }
                }

                
                return videoId;
            };

            Get["/id", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];
                var song = (await spotifyService.GetCurrentSong(token));
                if (song != null && song.item != null)
                {
                    return song.item.id;
                }
                return null;
            };
            Get["/search", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];
                string mode = this.Request.Query["mode"];
                var song = (await spotifyService.GetCurrentSong(token));
                return await youTubeGoogleService.GetSearchResults(song, mode, 4);
            };
            //Get["/pause/{id}", true] = async (parameters, ct) => await spotifyService.PauseSong(parameters["id"]);

            Get["/join/asd/", true] = async (parameters, ct) =>
            {
                string token = this.Request.Query["token"];

                var song =  (await spotifyService.GetCurrentSong(token));

                return song.item.id;
            };

        }
    }
}
