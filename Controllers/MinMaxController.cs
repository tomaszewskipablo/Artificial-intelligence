using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ArtificialIntelligence.Controllers
{
    public class MinMaxController : Controller
    {
        public IActionResult Index()
        {
            return View("MinMax");
        }
    }
}