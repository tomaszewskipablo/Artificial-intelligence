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

            Algorithm algorithm = new Algorithm();

            var algorithmSort = formCollection["algorithm"];
            if (algorithmSort == "hillClimbing")
            {
                HillClimbingSolution hillClimbingSolution = new HillClimbingSolution();
                algorithm.SetSolution(hillClimbingSolution);
            }
            else if(algorithmSort == "simulatedAnnealing")
            {
                algorithm.param1 = int.Parse(formCollection["coolingFactor"]);
                algorithm.param2 = int.Parse(formCollection["startingTemperature"]);

                simulatedAnnealingSolution simulatedAnnealingSolution = new simulatedAnnealingSolution();
                algorithm.SetSolution(simulatedAnnealingSolution);
            }
            else if (algorithmSort == "localBeamSearch")
            {
                algorithm.param1 = int.Parse(formCollection["numberOfStates"]);

                localBeamSearchSolution localBeamSearchSolution = new localBeamSearchSolution();
                algorithm.SetSolution(localBeamSearchSolution);
            }
            else  // geneticAlgorithms
            {
                algorithm.param1 = int.Parse(formCollection["sizeOfASingleGeneration"]);
                algorithm.param2 = int.Parse(formCollection["percentOfElitism"]);
                algorithm.param3 = int.Parse(formCollection["crossoverProbability"]);
                algorithm.param4 = int.Parse(formCollection["mutationProbability"]);
                algorithm.param5 = int.Parse(formCollection["numberOfGenerations"]);

                GeneticAlgorithmSolution geneticAlgorithmSolution = new GeneticAlgorithmSolution();
                algorithm.SetSolution(geneticAlgorithmSolution);
            }

            Chessboard chessboard = new Chessboard(size);
            chessboard.randomizeChessboard();
            algorithm.SetChessboard(chessboard);


            return View("Index", algorithm.GetChessboard()); ;           
        }

        public IActionResult displayChessboard()
        {
            Chessboard chessboard = new Chessboard(9);
            return View(chessboard);
        }
    }
}