using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.UseCase
{
    public class PostSuggestionModel
    {
        public string VideoId { get; set; }
        public string SongId { get; set; }
        public Guid ModeId { get; set; }
        public Guid UserId { get; set; }
    }
}
