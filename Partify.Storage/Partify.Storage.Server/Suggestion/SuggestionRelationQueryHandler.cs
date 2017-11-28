using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Partify.Storage.Server.CQRS;
using System.Data;
using System;

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
            if (query.UserId == Guid.Empty)
            {
                var mostPopularSuggestion = await m_dbConnection.QueryAsync<SuggestionRelationResult>(Sql.SuggestionByMostPopular, query);
                return mostPopularSuggestion;
            }
            var result = await m_dbConnection.QueryAsync<SuggestionRelationResult>(Sql.SuggestionByIds, query);
            return result;
        }
    }
}
