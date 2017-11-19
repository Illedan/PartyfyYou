using System;

namespace Partify.Storage.Server.UserSuggestion
{
    public class CreateUserSuggestionCommand
    {
        public Guid Id { get; set; }
        public Guid SuggestionId { get; set; }
        public Guid UserId { get; set; }
    }
}