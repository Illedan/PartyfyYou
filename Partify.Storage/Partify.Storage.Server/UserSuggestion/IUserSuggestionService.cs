using System.Threading.Tasks;

namespace Partify.Storage.Server.UserSuggestion
{
    public interface IUserSuggestionService
    {
        Task Post(CreateUserSuggestionRequest createUserSuggestionRequest);
    }
}