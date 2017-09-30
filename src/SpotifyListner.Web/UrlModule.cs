using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Extensions;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        private static string URL;
        public UrlModule()
        {
            Get["/url"] = parameters => !string.IsNullOrEmpty(URL) ? URL : "https://www.youtube.com/embed/o1eHKf-dMwo?autoplay=1";
            Post["/url"] = parameters =>
            {
                var text = Context.Request.Body.AsString();
                URL = text;
                return true;
            };
        }
    }

}
