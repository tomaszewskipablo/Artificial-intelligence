using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models;
using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public IActionResult NewSize(IFormCollection formCollection)
        {            
                int size = int.Parse(formCollection["size"]);

            var algorithm = formCollection["algorithm"];
            if (algorithm == "hillClimbing")
            {
                
            }
            else if(algorithm == "simulatedAnnealing")
            {
                var parm1 = formCollection["firVar"];
                var parm2 = formCollection["secVar"];
            }
            else if (algorithm == "localBeamSearch")
            {
                var parm1 = formCollection["firVar"];
            }
            else  // geneticAlgorithms
            {
                var parm1 = formCollection["firVar"];
                var parm2 = formCollection["secVar"];
                var parm3 = formCollection["thiVar"];
                var parm4 = formCollection["fouVar"];
            }


            Chessboard chessboard = new Chessboard(size);
                return View("Index", chessboard);           
        }

        public IActionResult displayChessboard()
        {
            Chessboard chessboard = new Chessboard(9);
            return View(chessboard);
        }
    }
}