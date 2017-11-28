BEGIN
   IF NOT EXISTS (select SpotifySong.Id, SpotifySong.SongId, YoutubeVideo.Id, YoutubeVideo.VideoId, Mode.Name
					from (((SpotifySong
					inner join Suggestion on Suggestion.SpotifyIdFK = SpotifySong.Id)
					inner join YoutubeVideo on Suggestion.YoutubeIdFK = YoutubeVideo.Id) 
					inner join Mode on Suggestion.ModeIdFK = Mode.Id)
					where YoutubeVideo.VideoId = @VideoId and SpotifySong.SongId = @SongId and Mode.Name = @ModeName)
   BEGIN
		DECLARE @SpotifyId uniqueidentifyer
		SELECT @SpotifyId = (select SpotifySong.Id from SpotifySong where SpotifySong.SongId = @SongId)

		DECLARE @YoutubeId uniqueidentifyer
		SELECT @YoutubeId = (select YoutubeVideo.Id from YoutubeVideo where YoutubeVideo.VideoId = @VideoId)

		DECLARE @ModeId uniqueidentifyer
		SELECT @ModeId = (select Mode.Id from Mode where Mode.Name = @ModeName)
		

       INSERT INTO Suggestion
		(Id, SpotifyIdFK, YoutubeIdFK, Count, Overruled, ModeIdFK) 
		VALUES (@Id, @SpotifyId, @YoutubeId, 1, 0, @ModeId)
		--RETURN 1

   END
   --RETURN 0;
END