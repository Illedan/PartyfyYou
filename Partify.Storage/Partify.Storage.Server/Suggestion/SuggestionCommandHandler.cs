using Dapper;
using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Suggestion
{
    public class UserCommandHandler : ICommandHandler<SuggestionCommand>
    {
        private readonly IDbConnection m_dbConnection;
        public UserCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task HandleAsync(SuggestionCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostSuggestion, command);
        }
    }
}
