using Dapper;
using Partify.Storage.Server.CQRS;
using System.Data;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Suggestion
{
    public class DeleteSuggestionCommandHandler : ICommandHandler<DeleteSuggestionCommand>
    {
        private readonly IDbConnection m_dbConnection;
        public DeleteSuggestionCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task HandleAsync(DeleteSuggestionCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.DeleteSuggestionById, command);
        }
    }
}
