using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Partify.Storage.Server.CQRS;
using System.Data;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionQueryHandler : IQueryHandler<SuggestionQuery, IEnumerable<SuggestionResult>>
    {
        private readonly IDbConnection m_dbConnection;

        public SuggestionQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task<IEnumerable<SuggestionResult>> HandleAsync(SuggestionQuery query)
        {
            var result = await m_dbConnection.QueryAsync<SuggestionResult>(Sql.SuggestionByIds, query);
            return result;
        }
    }
}
