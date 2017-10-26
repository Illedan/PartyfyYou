using Nancy;

namespace SpotifyListner.Web
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = parameters =>
            {
                return Response.AsFile("Content/views/index.html", "text/html");
            };
            Get["/okpage"] = parameters =>
            {
                return Response.AsFile("Content/views/okpage.html", "text/html");
            };
        }
    }
}
