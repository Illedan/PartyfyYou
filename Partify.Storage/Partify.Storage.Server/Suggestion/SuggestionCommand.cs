using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionCommand
    {
        public Guid Id { get; set; }
        public Guid SpotifyId { get; set; }
        public Guid YoutubeId { get; set; }
        public Guid ModeId { get; set; }
        public int Count { get; set; }
        public bool Overruled { get; set; }
    }
}
