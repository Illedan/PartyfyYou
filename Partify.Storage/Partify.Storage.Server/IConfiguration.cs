using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server
{
    public interface IConfiguration
    {
        string ConnectionString { get; set; }
    }
}
