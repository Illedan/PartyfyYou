using System;

namespace Partify.Storage.Suggestion
{
    public class SuggestionResult
    {
        public Guid Id { get; set; }
        public Guid SongId { get; set; }
        public Guid VideoId { get; set; }
        public Guid ModeId { get; set; }
        public int Count { get; set; }
        public bool Overruled { get; set; }
    }
}