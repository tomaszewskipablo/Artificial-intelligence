using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtificialIntelligence.Models;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ArtificialIntelligence.Controllers
{
    public class NQueensProblemController : Controller
    {

        public IActionResult Index()
        {
            
            Algorithm algorithm = Algorithm.Instance;
            Chessboard chessboard = new Chessboard(6);
            algorithm.SetChessboard(chessboard);

            return View("Index", algorithm); 
        }

        [HttpPost]
        public IActionResult DoAlgorithm(IFormCollection formCollection)
        {
            Algorithm algorithm = Algorithm.Instance;


            var algorithmSort = formCollection["algorithm"];
            if (algorithmSort == "hillClimbing")
            {
                HillClimbingParameters parameters = new HillClimbingParameters();
                parameters.maxNumberOfSteps = int.Parse(formCollection["maxNumberOfSteps"]);
                algorithm.SetParameters(parameters);
                HillClimbingSolution hillClimbingSolution = new HillClimbingSolution();
                algorithm.SetSolution(hillClimbingSolution);
            }
            else if (algorithmSort == "simulatedAnnealing")
            {
                simulatedAnnealingSolution simulatedAnnealingSolution = new simulatedAnnealingSolution();
                SimulatedAnnealingParameters parameters = new SimulatedAnnealingParameters();
                parameters.coolingFactor = int.Parse(formCollection["coolingFactor"]);
                parameters.startingTemperature = int.Parse(formCollection["startingTemperature"]);
                algorithm.SetSolution(simulatedAnnealingSolution);
                algorithm.SetParameters(parameters);
            }
            else if (algorithmSort == "localBeamSearch")
            {
                localBeamSearchParameters parameters = new localBeamSearchParameters();
                parameters.numberOfStates = int.Parse(formCollection["numberOfStates"]);
                algorithm.SetParameters(parameters);
                localBeamSearchSolution localBeamSearchSolution = new localBeamSearchSolution();
                algorithm.SetSolution(localBeamSearchSolution);
            }
            else  // geneticAlgorithms
            {
                GeneticAlgorithmParameters parameters = new GeneticAlgorithmParameters();
                parameters.sizeOfASingleGeneration = int.Parse(formCollection["sizeOfASingleGeneration"]);
                parameters.percentOfElitism = int.Parse(formCollection["percentOfElitism"]);
                parameters.crossoverProbability = int.Parse(formCollection["crossoverProbability"]);
                parameters.mutationProbability = int.Parse(formCollection["mutationProbability"]);
                parameters.numberOfGenerations = int.Parse(formCollection["numberOfGenerations"]);

                algorithm.SetParameters(parameters);

                GeneticAlgorithmSolution geneticAlgorithmSolution = new GeneticAlgorithmSolution();
                algorithm.SetSolution(geneticAlgorithmSolution);
                //algorithm.name = "Genetic Algorithm";
            }

            // DO ALL ALGORITHMS HERE
            algorithm.SolveProblem();

            return View("Index", algorithm); ;
        }

        [HttpPost]
        public IActionResult RandomChessboard(IFormCollection formCollection)
        {

            int size = int.Parse(formCollection["size"]);
        
            Chessboard chessboard = new Chessboard(size);
            chessboard.randomizeChessboard();
            

            Algorithm algorithm = Algorithm.Instance;
            algorithm.SetChessboard(chessboard);


            return View("algorithm", algorithm); ;
        }


        public IActionResult displayChessboard()
        {
            Chessboard chessboard = new Chessboard(9);
            return View(chessboard);
        }

    }
}