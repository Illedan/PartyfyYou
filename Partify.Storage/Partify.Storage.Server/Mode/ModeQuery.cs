using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server.Mode
{
    public class ModeQuery : IQuery<IEnumerable<ModeResult>>
    {
        public Guid Id { get; set; }
    }
}
