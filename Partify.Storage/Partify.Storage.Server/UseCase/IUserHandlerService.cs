using Partify.Storage.Server.Suggestion;
using Partify.Storage.Server.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.UseCase
{
    public interface IUserHandlerService
    {
        Task<UserResult> HandleUser(UserModel user);
    }
}
