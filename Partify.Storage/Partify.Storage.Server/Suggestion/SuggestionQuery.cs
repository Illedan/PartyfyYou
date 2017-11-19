using Partify.Storage.Server.CQRS;
using System.Collections.Generic;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionQuery : IQuery<IEnumerable<SuggestionResult>>
    {
        public string VideoId { get; set; }
        public string SongId { get; set; }
        public string ModeName { get; set; }
        public string UserName { get; set; }
    }
}
