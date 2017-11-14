using System;
using System.Collections.Generic;
using System.Text;

namespace Partify.Storage.Server
{
    public class PartifyConfiguration : IConfiguration
    {
        public PartifyConfiguration()
        {

        }
        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
