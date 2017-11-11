using Partify.Storage.Query;
using System;

namespace Partify.Storage.Suggestion
{
    public class SuggestionQuery : IQuery<SuggestionResult>
    {
        public Guid Id { get; set; }
    }
}
