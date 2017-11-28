using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionQuery : IQuery<SuggestionResult>
    {
        public Guid ModeId { get; set; }

        public Guid YoutubeId { get; set; }

        public Guid SpotifyId { get; set; }
    }
}
