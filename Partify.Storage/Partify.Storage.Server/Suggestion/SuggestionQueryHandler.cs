using Dapper;
using System.Threading.Tasks;
using Partify.Storage.Server.CQRS;
using System.Data;
using System.Linq;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionQueryHandler : IQueryHandler<SuggestionQuery, SuggestionResult>
    {
        private readonly IDbConnection m_dbConnection;

        public SuggestionQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task<SuggestionResult> HandleAsync(SuggestionQuery query)
        {
            var result = await m_dbConnection.QueryAsync<SuggestionResult>(Sql.GetSuggestion,query);

            return result.FirstOrDefault();
        }
    }
}
