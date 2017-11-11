using Dapper;
using Partify.Storage.Server.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            if (string.IsNullOrEmpty(query.Id))
            {
                var result = await m_dbConnection.QueryAsync<ModeResult>(Sql.AllModes);
                return result;
            }
            else
            {
                var result = await m_dbConnection.QueryAsync<ModeResult>(Sql.AllModes, new { query.Id });
                return result;
            }
        }
    }
}
