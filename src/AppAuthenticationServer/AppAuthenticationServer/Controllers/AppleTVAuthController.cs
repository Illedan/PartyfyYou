using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppAuthenticationServer.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAuthenticationServer.Controllers
{
    [Route("/appletv/")]
    public class AppleTVAuthController : Controller
    {
        readonly AuthService authService;

        public AppleTVAuthController(AuthService authService) => this.authService = authService;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string appleTVKey)
        {



            return Content($"Hello {appleTVKey}");
        }    
    }
}
