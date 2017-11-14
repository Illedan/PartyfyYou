
using Dapper;
using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
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
            var result = await m_dbConnection.QueryAsync<ModeResult>("SELECT [Id] ,[Name] FROM [dbo].[Mode]");
            return result;
        }
    }
}
