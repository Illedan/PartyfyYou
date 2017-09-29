using Topshelf.Nancy;
using Topshelf;

namespace SpotifyListner.Web
{
    public class Program
    {
        private const int Port = 1337;

        static void Main(string[] args)
        {
            var host = HostFactory.New(x =>
            {
                x.UseLinuxIfAvailable();
                x.Service<SpotifyListnerSelfHost>(s =>
                {
                    s.ConstructUsing(settings => new SpotifyListnerSelfHost($"http://+:{Port}"));
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
