﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LayoutNull()
        {
            return View();
        }

        public IActionResult LayoutNull2()
        {
            return View();
        }

        //[Models.Shared.Menu(Key = -1)]
        public IActionResult Layout()
        {
            return View();
        }

        public IActionResult SignalR()
        {
            return View();
        }

        public IActionResult Badge()
        {
            return View();
        }
    }
}
