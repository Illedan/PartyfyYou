select 
	Id,
	SpotifyIdFk as SpotifyId,
	YoutubeIdFk as YoutubeId,
	ModeIdFk as ModeId,
	Overruled,
	Count
from Suggestion
where
	ModeIdFk = @ModeId and
	YoutubeIdFk = @YoutubeId and
	SpotifyIdFk = @SpotifyId