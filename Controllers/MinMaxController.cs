using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public struct Field
{
    public int x;
    public int y;

    public Field(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

namespace ArtificialIntelligence.Controllers
{
    public class MinMaxController : Controller
    {
        public IActionResult Index()
        {

            GameState gameState = GameState.Instance;
            gameState.StartGame();
            if (gameState.GetIsAImove())
            {
                Field field = gameState.AIMove();
                gameState.MakeMove(field);
            }

            return View("MinMax", gameState);
        }

        [HttpPost]
        public IActionResult Game(IFormCollection formCollection)
        {
            GameState gameState = GameState.Instance;

            Field field;
            if (!gameState.CheckGameStatus())
            {
                // ------- USER --------
                int fieldUser = int.Parse(formCollection["Move"]);
                field = new Field(fieldUser / 3, fieldUser % 3);
                gameState.MakeMove(field);
                // ------- USER --------
            }

            if (!gameState.CheckGameStatus())
            {
                // -------  AI  --------
                field = gameState.AIMove();
                gameState.MakeMove(field);
                // -------  AI  --------
            }

            gameState.CheckGameStatus();
            return View("MinMax", gameState);
        }

        [HttpPost]
        public IActionResult Restart(IFormCollection formCollection)
        {
            GameState gameState = GameState.Instance;

            gameState.RestartGame();

            return View("MinMax", gameState);
        }
    }
}
