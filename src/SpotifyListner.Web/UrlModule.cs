using Nancy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyListner.Web
{
    public class UrlModule : NancyModule
    {
        public UrlModule()
        {
            Get["/url"] = parameters =>
            {
                if (File.Exists("c:\\videoLink.txt"))
                {
                    var res = File.ReadAllText("c:\\videoLink.txt");
                    return res;

                }
                return "https://www.youtube.com/embed/o1eHKf-dMwo?autoplay=1";
            };
        }
    }

}
