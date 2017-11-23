using Dapper;
using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.UserSuggestion
{
    public class DeleteUserSuggestionCommandHandler : ICommandHandler<DeleteUserSuggestionCommand>
    {
        private readonly IDbConnection m_dbConnection;
        public DeleteUserSuggestionCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task HandleAsync(DeleteUserSuggestionCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.DeleteUserSuggestionById, command);
        }
    }
}
