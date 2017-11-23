using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Storage.Server.UseCase;
using Partify.Storage.Server.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Partify.Storage.WebApi.Controller
{
    [Route("api/[controller]")]
    public class User : ControllerBase
    {
        private readonly IUserHandlerService m_userHandlerService;
        public User(IUserHandlerService userHandlerService)
        {
            m_userHandlerService = userHandlerService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResult), 200)]
        public async Task<UserResult> Post([FromBody]UserModel user)
        {
            var result = await m_userHandlerService.HandleUser(user);
            return result;
        }

    }
}
