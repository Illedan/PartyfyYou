using Partify.Storage.Server.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Mode
{
    public class ModeService : IModeService
    {
        private readonly IQueryExecutor m_queryExecutor;
        public ModeService(IQueryExecutor queryExecutor)
        {
            m_queryExecutor = queryExecutor;
        }
        public async Task<IEnumerable<ModeResult>> GetAllModes()
        {
            var result = await m_queryExecutor.ExecuteAsync(new ModeQuery());
            return result;
        }
    }
}
