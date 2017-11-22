using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Partify.Storage.Server.Suggestion;
using Partify.Storage.Server.Video;
using Partify.Storage.Server.SpotifySong;
using Partify.Storage.Server.Song;
using Partify.Storage.Server.UserSuggestion;

namespace Partify.Storage.Server.UseCase
{
    public class SuggestionHandlerService : ISuggestionHandlerService
    {
        private readonly IVideoService m_videoService;
        private readonly ISongService m_songService;
        private readonly ISuggestionService m_suggestionService;
        private readonly IUserSuggestionService m_userSuggestionService;

        private readonly Guid PartifySystemUserId = new Guid("6E952A43-0FFF-4B5A-A008-480A8BB0E8EE");
        public SuggestionHandlerService(IVideoService videoService, ISongService songService, ISuggestionService suggestionService, IUserSuggestionService userSuggestionService)
        {
            m_videoService = videoService;
            m_songService = songService;
            m_suggestionService = suggestionService;
            m_userSuggestionService = userSuggestionService;
        }

        public async Task AddSuggestion(string videoId, string songId, Guid modeId, Guid userId)
        {
            var video = await m_videoService.GetVideo(videoId);
            if (video == null)
            {
                await m_videoService.PostVideo(new CreateVideoRequest { VideoId = videoId});
                video = await m_videoService.GetVideo(videoId);
            }
            var song = await m_songService.Get(songId);
            if (song == null)
            {
                await m_songService.Post(new CreateSongRequest {SongId = songId});
                song = await m_songService.Get(songId);
            }
            var suggestionRelation = await m_suggestionService.GetSuggestionRelation(new SuggestionRelationRequest {ModeId = modeId, SongId = song.SongId, UserId = userId });
            if (suggestionRelation == null) // The user does not have this suggestion
            {
                suggestionRelation = await m_suggestionService.GetSuggestionRelation(new SuggestionRelationRequest { ModeId = modeId, SongId = song.SongId, UserId = PartifySystemUserId }); // Partify-System userId
                if (suggestionRelation==null)// this is not suggested by the Partify-System user. (or anybody else)
                {
                    // post to relation and userRelation for the User and Partify-System user
                    await m_suggestionService.PostSuggestion(new CreateSuggestionRequest {ModeId = modeId, Overruled = false, SpotifyId = song.Id, YoutubeId = video.Id });
                    var suggestion = await m_suggestionService.GetSuggestion(song.Id, video.Id, modeId);
                    await m_userSuggestionService.Post(new CreateUserSuggestionRequest { SuggestionId = suggestion.Id, UserId = userId});
                    await m_userSuggestionService.Post(new CreateUserSuggestionRequest { SuggestionId = suggestion.Id, UserId = PartifySystemUserId });
                    return;
                }
                // The suggestion is set by the Partify-System user and we only need to post it to the UserSuggestion table
                var suggestionByPartifySystemUser = await m_suggestionService.GetSuggestion(song.Id, video.Id, modeId);
                await m_userSuggestionService.Post(new CreateUserSuggestionRequest { SuggestionId = suggestionByPartifySystemUser.Id, UserId = userId });
                return;
               
            }
            
            // the user have allready added a suggestion for him/herserlf, we should now chance the prefered suggestion for that spesific user
            //delete from UserSuggestion where UserSuggestion.Id = suggestionRelation.UserSuggestionId
            // post to UserSuggestion with updated values
            // await m_userSuggestionService.Post(new CreateUserSuggestionRequest { SuggestionId = suggestionByPartifySystemUser.Id, UserId = userId });
            

        }

        public Task<SuggestionRelationResult> GetSuggestion(SuggestionRelationRequest suggestion)
        {
            throw new NotImplementedException();
        }

        public Task RemoveSuggestion(string videoId, string songId, Guid modeId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceSuggestion(string newVideoId, string songId, Guid modeId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
