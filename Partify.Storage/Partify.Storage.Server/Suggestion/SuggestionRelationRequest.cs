using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionRelationRequest
    {
        public string SongId { get; set;}
        public Guid ModeId { get; set; }  
        public Guid UserId { get; set; }
    }
}
