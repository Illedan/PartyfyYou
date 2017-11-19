using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Partify.Storage.Server.User
{
    public interface IUserService
    {
        Task<UserResult> Get(Guid id);
        Task<IEnumerable<UserResult>> GetAll();
        Task Post(CreateUserRequest createUserRequest);
    }
}