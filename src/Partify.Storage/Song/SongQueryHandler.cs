using Partify.Storage.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Song
{
    public class SongQueryHandler : IQueryHandler<SongQuery, SongResult>
    {



        public async Task<SongResult> HandleAsync(SongQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
