using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Partify.Storage.SelfHost
{
    class Program
    {
        private static int Main()
        {

            return (int)HostFactory.Run(
                 host =>
                 {
                     host.UseLinuxIfAvailable();
                     host.Service<WebApplication>(
                         service =>
                         {
                             service.ConstructUsing(() => new WebApplication());
                             service.WhenStarted(s => s.Start());
                             service.WhenStopped(s => s.Stop());
                         });
                     host.RunAsLocalSystem();
                     host.SetServiceName("PartifyStorage");
                     host.SetDisplayName("Partify Storage");
                     host.SetDescription("WebService for storing Partify data");
                 });
        }
    }
}
