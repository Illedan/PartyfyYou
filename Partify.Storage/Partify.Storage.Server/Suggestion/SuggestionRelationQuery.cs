using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionRelationQuery : IQuery<IEnumerable<SuggestionRelationResult>>
    {
        public string SongId { get; set; }
        public Guid ModeId { get; set; }
        public Guid UserId { get; set; }
    }
}
