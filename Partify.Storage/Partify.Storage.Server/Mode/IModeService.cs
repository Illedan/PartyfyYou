using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partify.Storage.Server.Mode
{
    public interface IModeService
    {
        Task<IEnumerable<ModeResult>> GetAllModes();
    }
}
