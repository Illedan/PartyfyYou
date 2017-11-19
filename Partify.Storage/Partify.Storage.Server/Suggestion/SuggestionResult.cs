using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionResult
    {
        public Guid Id { get; set; }
        public Guid SpotifyId { get; set; }
        public string SpotifySongId { get; set; }
        public Guid YoutubeId { get; set; }
        public string YoutubeVideoId { get; set; }
        public Guid ModeId { get; set; }
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string Mode { get; set; }
        public int Count { get; set; }
        public bool Overruled { get; set; }
    }
}
