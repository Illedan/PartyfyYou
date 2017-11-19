using Dapper;
using Partify.Storage.Server.CQRS;
using System.Data;
using System.Threading.Tasks;

namespace Partify.Storage.Server.User
{
    public class UserCommandHandler : ICommandHandler<UserCommand>
    {
        private readonly IDbConnection m_dbConnection;
        public UserCommandHandler(IDbConnection dbConnection)
        {
            m_dbConnection = dbConnection;
        }
        public async Task HandleAsync(UserCommand command)
        {
            await m_dbConnection.ExecuteAsync(Sql.PostUser, command);
        }
    }
}
