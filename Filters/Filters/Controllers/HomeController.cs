using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Filters.Infrastructure;

namespace Filters.Controllers
{
    [HttpsOnly]
    public class HomeController : Controller
    {
        public ViewResult Index() => View("Message",
            "This is the Index action on the Ноmе controller");

        public ViewResult SecondAction() => View("Message",
            "This is the SecondAction on the Ноmе controller");
    }
}