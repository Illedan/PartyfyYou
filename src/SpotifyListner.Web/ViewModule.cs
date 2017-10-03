using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web
{
    public class ViewModule : NancyModule
    {
        public ViewModule()
        {
            //After.AddItemToEndOfPipeline((ctx) => ctx.Response
            //    .WithHeader("Access-Control-Allow-Origin", "*")
            //    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
            //    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"));

            Get["/help"] = parameters =>
            {
                return Response.AsFile("Content/views/help.html", "text/html");
            };

            Get["/join"] = parameters =>
            {
                return Response.AsFile("Content/views/join.html", "text/html");
            };
        }
    }
}
