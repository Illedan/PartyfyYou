using LightInject;
using SpotifyListner.Web.Services;

namespace SpotifyListner.Web
{
    public class CompositionRoot : ICompositionRoot
    {
        void ICompositionRoot.Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IYouTubeGoogleService, YouTubeGoogleService>();
        }
    }
}