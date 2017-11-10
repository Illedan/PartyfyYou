using LightInject;
using SpotifyListner.Web.Services;
using SpotifyListner.Web.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISpotifyWebApi, SpotifyWebApi>();
            serviceRegistry.Register<IAuthorization, Authorization>();
            serviceRegistry.Register<IKeyService, KeyService>();
            serviceRegistry.Register<ISpotifyService, SpotifyService>();
            serviceRegistry.Register<IYouTubeGoogleService, YouTubeGoogleService>();
        }
    }
}
