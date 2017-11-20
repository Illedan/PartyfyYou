using Dapper;
using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Mode
{
    public class ModeQueryHandler : IQueryHandler<ModeQuery, IEnumerable<ModeResult>>
    {
        private readonly IDbConnection m_dbConnection;

        public ModeQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task<IEnumerable<ModeResult>> HandleAsync(ModeQuery query)
        {
            if (query.Id == Guid.Empty)
            {
               return await m_dbConnection.QueryAsync<ModeResult>(Sql.AllModes);
            }

            return await m_dbConnection.QueryAsync<ModeResult>(Sql.ModeById, query);
        }
    }
}
