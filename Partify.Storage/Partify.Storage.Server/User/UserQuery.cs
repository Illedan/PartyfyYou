using System;
using Partify.Storage.Server.CQRS;
using System.Collections.Generic;

namespace Partify.Storage.Server.User
{
    public class UserQuery : IQuery<IEnumerable<UserResult>>
    {
        public Guid Id { get; set; }
    }
}