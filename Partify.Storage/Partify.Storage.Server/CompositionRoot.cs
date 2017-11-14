using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightInject;
using Partify.Storage.Server.Mode;

namespace Partify.Storage.Server
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IModeService, ModeService>(new PerScopeLifetime());
        }
    }
}
