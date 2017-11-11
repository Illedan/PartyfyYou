using Partify.Storage.Server.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Mode
{
    public class ModeQuery : IQuery<IEnumerable<ModeResult>>
    {
        public string Id { get; set; }
    }
}
