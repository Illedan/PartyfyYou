using Dapper;
using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Partify.Storage.Server.User
{
    public class UserQueryHandler : IQueryHandler<UserQuery, IEnumerable<UserResult>>
    {
        private readonly IDbConnection m_dbConnection;

        public UserQueryHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }

        public async Task<IEnumerable<UserResult>> HandleAsync(UserQuery query)
        {
            if (query.Id != Guid.Empty)
            {
                return await m_dbConnection.QueryAsync<UserResult>(Sql.UserById, query);
            }
            if (!string.IsNullOrEmpty(query.SpotifyUserId))
            {
                return await m_dbConnection.QueryAsync<UserResult>(Sql.UserBySpotifyId, query);
            }

            return await m_dbConnection.QueryAsync<UserResult>(Sql.AllUsers);
        }
    }
}
