using Nancy;

namespace SpotifyListner.Web
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            //After.AddItemToEndOfPipeline((ctx) => ctx.Response
            //    .WithHeader("Access-Control-Allow-Origin", "*")
            //    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            //    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

            Get["/"] = parameters =>
            {

                return Response.AsFile("index.html", "text/html");
            };
        }
    }
}
