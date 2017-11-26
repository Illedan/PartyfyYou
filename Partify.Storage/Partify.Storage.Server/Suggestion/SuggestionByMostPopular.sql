select TOP (1)
	count(*) as [Count],
	Suggestion.Id as Id,
	Suggestion.YoutubeIdFK as YoutubeId,
	(select YoutubeVideo.VideoId from YoutubeVideo where YoutubeVideo.Id = Suggestion.YoutubeIdFK) as YoutubeVideoId
from 
	UserSuggestion, 
	Suggestion
	
where 
EXISTS
	(select * from SpotifySong where SpotifySong.SongId = @SongId and SpotifySong.Id = Suggestion.SpotifyIdFK) and
EXISTS 
	(select * from Mode where Mode.Id = @ModeId and Mode.Id = Suggestion.ModeIdFK) and
Suggestion.Id = UserSuggestion.SuggestionIdFK group by
	Suggestion.Id, Suggestion.YoutubeIdFK order by [Count] desc