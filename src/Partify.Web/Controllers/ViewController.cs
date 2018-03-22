using Microsoft.AspNetCore.Mvc;

namespace Partify.Web.Controllers
{
    //[Route(/")]
    public class ViewController : Controller
    {
        [HttpGet("/")]
        public ActionResult Get()
        {
            return View("/Content/views/index.html");
        }

        [HttpGet("/videopage")]
        public ActionResult Get1()
        {
            return View("/Content/views/videopage.html");
        }

        [HttpGet("/callback")]
        public ActionResult Get2()
        {
            return View("/Content/views/callback.html");
        }
    }
}