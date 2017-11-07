using System.Configuration;
using Topshelf.Nancy;
using Topshelf;

namespace SpotifyListner.Web
{
    public class Program
    {
        private static int Port;

        static void Main(string[] args)
        {
            var portString = ConfigurationManager.AppSettings["Port"];
            Port = int.Parse(portString);

            var host = HostFactory.New(x =>
            {
                x.UseLinuxIfAvailable();
                x.Service<SpotifyListnerSelfHost>(s =>
                {
                    s.ConstructUsing(settings => new SpotifyListnerSelfHost(ConfigurationManager.AppSettings["HostUrl"]));
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                    s.WithNancyEndpoint(x, c =>
                    {
                        c.AddHost(port: Port);
                        c.CreateUrlReservationsOnInstall();
                        c.OpenFirewallPortsOnInstall(firewallRuleName: "SpotifyListner");
                    });
                });

                x.StartAutomatically();
                x.SetServiceName("SpotifyListner");
                x.SetDisplayName("SpotifyListner");
                x.SetDescription("SpotifyListner");
                x.RunAsNetworkService();

            });

            host.Run();
        }
    }
}