using System;

namespace Partify.Storage.Server.UserSuggestion
{
    public class CreateUserSuggestionRequest
    {
        public Guid SuggestionId { get; internal set; }
        public Guid UserId { get; internal set; }
    }
}