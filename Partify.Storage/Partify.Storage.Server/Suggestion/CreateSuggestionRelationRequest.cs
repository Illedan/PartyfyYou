using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Suggestion
{
    public class CreateSuggestionRelationRequest
    {
        public string SongId { get; set; }
        public string VideoId { get; set; }
        public string ModeName { get; set; }
    }
}
