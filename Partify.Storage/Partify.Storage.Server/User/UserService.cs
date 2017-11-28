using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.User
{
    public class UserService : IUserService
    {
        private readonly IQueryExecutor m_queryExecutor;
        private readonly ICommandExecutor m_commandExecutor;

        public UserService(IQueryExecutor queryExecutor, ICommandExecutor commandExecutor)
        {
            m_queryExecutor = queryExecutor;
            m_commandExecutor = commandExecutor;
        }


        public async Task<UserResult> Get(Guid id)
        {
            var result = await m_queryExecutor.ExecuteAsync(new UserQuery { Id = id });
            return result.FirstOrDefault();
        }

        public async Task<UserResult> Get(string spotifyUserId)
        {
            var result = await m_queryExecutor.ExecuteAsync(new UserQuery { SpotifyUserId = spotifyUserId });
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<UserResult>> GetAll()
        {
            var result = await m_queryExecutor.ExecuteAsync(new UserQuery());
            return result;
        }

        public async Task Post(CreateUserRequest createUserRequest)
        {
            var createUserCommand = new UserCommand
            {
                Id = Guid.NewGuid(),
                Name = createUserRequest.Name,
                Country = createUserRequest.Country,
                SpotifyUserId = createUserRequest.SpotifyUserId
            };

            await m_commandExecutor.ExecuteAsync(createUserCommand);
        }
    }
}
