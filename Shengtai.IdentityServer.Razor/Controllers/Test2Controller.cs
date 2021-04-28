using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Controllers
{
    public class Test2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
