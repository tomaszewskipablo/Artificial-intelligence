using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

public struct Field{
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
            return View("MinMax", gameState);
        }

        [HttpPost]
        public IActionResult Game(IFormCollection formCollection)
        {
            GameState gameState = GameState.Instance;

            for (int i = 0; i < 2; i++)
            {
                Field field;
                if (gameState.GetIsAImove() == false)
                {
                    int fieldUser = int.Parse(formCollection["Move"]);

                    field = new Field(fieldUser / 3, fieldUser % 3);
                }
                else
                {
                    field = gameState.AIMove();
                }
                gameState.MakeMove(field);// check if space is free, if free save, if not dont save
                                          //gameState.CheckWin();
            }
            return View("MinMax", gameState);

        }
    }
}