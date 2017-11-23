using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.UserSuggestion
{
    public class UserSuggestionService : IUserSuggestionService
    {
        private readonly IQueryExecutor m_queryExecutor;
        private readonly ICommandExecutor m_commandExecutor;

        public UserSuggestionService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }

        public async Task Delete(Guid id)
        {
            await m_commandExecutor.ExecuteAsync(new DeleteUserSuggestionCommand { Id = id });
        }

        public async Task Post(CreateUserSuggestionRequest createUserSuggestionRequest)
        {
            var createUserSuggestionCommand = new UserSuggestionCommand
            {
                Id = Guid.NewGuid(),
                SuggestionId = createUserSuggestionRequest.SuggestionId,
                UserId = createUserSuggestionRequest.UserId
            };

            await m_commandExecutor.ExecuteAsync(createUserSuggestionCommand);
        }
    }
}
