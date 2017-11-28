using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Partify.Storage.Server.Suggestion;
using Partify.Storage.Server.User;

namespace Partify.Storage.Server.UseCase
{
    public class UserHandlerService : IUserHandlerService
    {
        private readonly IUserService m_userService;
        public UserHandlerService(IUserService userService)
        {
            m_userService = userService;
        }
        public async Task<UserResult> HandleUser(UserModel user)
        {
            var userResult = await m_userService.Get(user.SpotifyUserId);
            if (userResult == null)
            {
                await m_userService.Post(new CreateUserRequest
                {
                    Country = user.Contry,
                    Name = user.Name,
                    SpotifyUserId = user.SpotifyUserId
                });
                userResult = await m_userService.Get(user.SpotifyUserId);
            }
            return userResult;
        }
    }
}
