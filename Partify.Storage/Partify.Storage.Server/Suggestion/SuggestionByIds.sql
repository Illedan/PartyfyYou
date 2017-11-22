select  Suggestion.Id ,
	UserSuggestion.Id as UserSuggestionId,
	SpotifySong.Id as SpotifyId, 
	SpotifySong.SongId as SpotifySongId, 
	YoutubeVideo.Id as YoutubeId, 
	YoutubeVideo.VideoId as YoutubeVideoId, 
	Mode.Name as Mode, 
	Mode.Id as ModeId,
	[User].Name as UserName,
	[User].Id as UserId,
	Suggestion.Count, 
	Suggestion.Overruled 
					from (((((SpotifySong
						inner join Suggestion on Suggestion.SpotifyIdFK = SpotifySong.Id)
						inner join YoutubeVideo on Suggestion.YoutubeIdFK = YoutubeVideo.Id) 
						inner join Mode on Suggestion.ModeIdFK = Mode.Id)
						inner join UserSuggestion on Suggestion.Id = UserSuggestion.SuggestionIdFK)
						inner join [User] on UserSuggestion.UserIdFK = [User].Id)
					where SpotifySong.SongId = @SongId and Mode.Id = @ModeId and [User].Id = @UserId