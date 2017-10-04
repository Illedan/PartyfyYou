using Nancy;

namespace SpotifyListner.Web
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters =>
            {

                return Response.AsFile("index.html", "text/html");
            };
        }
    }
}
