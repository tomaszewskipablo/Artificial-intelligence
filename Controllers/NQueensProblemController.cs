using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models;
using Microsoft.AspNetCore.Mvc;


namespace ArtificialIntelligence.Controllers
{
    public class NQueensProblemController : Controller
    {

        public IActionResult Index()
        {

            Chessboard chessboard = new Chessboard(6);
            return View(chessboard);
            //return View();
        }
        public IActionResult displayChessboard()
        {
            Chessboard chessboard = new Chessboard(9);
            return View(chessboard);
        }
    }
}