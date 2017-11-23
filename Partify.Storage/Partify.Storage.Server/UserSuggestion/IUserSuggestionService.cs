using System;
using System.Threading.Tasks;

namespace Partify.Storage.Server.UserSuggestion
{
    public interface IUserSuggestionService
    {
        Task Post(CreateUserSuggestionRequest createUserSuggestionRequest);
        Task Delete(Guid id);
    }
}