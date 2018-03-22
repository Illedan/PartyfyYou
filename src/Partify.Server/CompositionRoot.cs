using LightInject;
using Partify.Server.Configuration;
using Partify.Server.Services;
using Partify.Server.Services.WebApi;

namespace Partify.Server
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISpotifyWebApi, SpotifyWebApi>(new PerScopeLifetime());
            serviceRegistry.Register<IAuthorization, Authorization>(new PerScopeLifetime());
            serviceRegistry.Register<IPartifyStorageService, PartifyStorageService>(new PerScopeLifetime());
            serviceRegistry.Register<IRestClientWrapper, RestClientWrapper>(new PerScopeLifetime());
            serviceRegistry.Register<ISpotifyService, SpotifyService>(new PerScopeLifetime());
            serviceRegistry.Register<IYouTubeGoogleService, YouTubeGoogleService>(new PerScopeLifetime());
        }
    }
}