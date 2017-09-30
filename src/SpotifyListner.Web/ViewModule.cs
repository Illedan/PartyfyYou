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
            Get["/help"] = parameters =>
            {
                return Response.AsFile("help.html", "text/html");
            };

            Get["/join"] = parameters =>
            {
                return Response.AsFile("join.html", "text/html");
            };
        }
    }
}
