using System;

namespace Partify.Server.Models
{
    public class PostSuggestionModel
    {
        public string VideoId { get; set; }
        public string SongId { get; set; }
        public Guid ModeId { get; set; }
        public Guid UserId { get; set; }
    }
}