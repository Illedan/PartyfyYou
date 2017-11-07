﻿using Nancy;
namespace SpotifyListner.Web
{
    public class ViewModule : NancyModule
    {
        public ViewModule()
        {
            Get["/videopage"] = parameters =>
            {
                return Response.AsFile("Content/views/videopage.html", "text/html");
            };
            Get["/callback"] = parameters =>
            {
                return Response.AsFile("Content/views/callback.html", "text/html");
            };
        }
    }
}
