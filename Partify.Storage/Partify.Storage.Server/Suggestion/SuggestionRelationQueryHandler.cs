using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Partify.Storage.Server.CQRS;
using System.Data;

namespace Partify.Storage.Server.Suggestion
{
    public class SuggestionRelationQueryHandler : IQueryHandler<SuggestionRelationQuery, IEnumerable<SuggestionRelationResult>>
    {
        private readonly IDbConnection m_dbConnection;

        public SuggestionRelationQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task<IEnumerable<SuggestionRelationResult>> HandleAsync(SuggestionRelationQuery query)
        {
            var result = await m_dbConnection.QueryAsync<SuggestionRelationResult>(Sql.SuggestionByIds, query);
            return result;
        }
    }
}
