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
            var results = await m_queryExecutor.ExecuteAsync(new ModeQuery());
            return results;
        }

        public async Task<ModeResult> GetModeById(Guid Id)
        {
            var results = await m_queryExecutor.ExecuteAsync(new ModeQuery {Id = Id });
            return results.FirstOrDefault();
        }
    }
}
