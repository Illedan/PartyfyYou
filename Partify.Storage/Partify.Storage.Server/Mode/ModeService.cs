using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Mode
{
    public class ModeService : IModeService
    {
        public async Task<List<ModeResult>> GetAllModes()
        {
            await Task.Delay(1);
            return new List<ModeResult> { new ModeResult { Id = Guid.NewGuid().ToString(), Name = "Karaoke" }, new ModeResult { Id = Guid.NewGuid().ToString(), Name = "Cover" } };
        }
    }
}
