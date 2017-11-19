using Partify.Storage.Server.CQRS;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Partify.Storage.Server.UserSuggestion
{
    public class CreateUserSuggestionCommandHandler : ICommandHandler<CreateUserSuggestionCommand>
    {
        private readonly IDbConnection m_dbConnection;

        public CreateUserSuggestionCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task HandleAsync(CreateUserSuggestionCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostUserSuggestion, command);
        }
    }
}