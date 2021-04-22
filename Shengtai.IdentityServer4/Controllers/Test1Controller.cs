using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Controllers
{
    public class Test1Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
