using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models.MinMax;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtificialIntelligence.Controllers
{
    public class MinMaxController : Controller
    {
        public IActionResult Index()
        {

            return View("MinMax");
        }

        [HttpPost]
        public IActionResult Game(IFormCollection formCollection)
        {
            GameState gameState = GameState.Instance;
            //var x = formCollection["x"];
            //var y = formCollection["Y"];

            // Model.MakeMove(x,y); check if space is free, if free save, if not dont save
            // Model.CheckWin()
            // Model.AIMove()

            return View("MinMax");

        }
    }
}