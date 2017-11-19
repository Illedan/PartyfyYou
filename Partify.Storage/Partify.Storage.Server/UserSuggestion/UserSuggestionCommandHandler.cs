using Partify.Storage.Server.CQRS;
using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Partify.Storage.Server.UserSuggestion
{
    public class UserSuggestionCommandHandler : ICommandHandler<UserSuggestionCommand>
    {
        private readonly IDbConnection m_dbConnection;

        public UserSuggestionCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task HandleAsync(UserSuggestionCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostUserSuggestion, command);
        }
    }
}