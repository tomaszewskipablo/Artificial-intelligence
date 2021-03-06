﻿using System;
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
            Chessboard chessboard = new Chessboard(4);
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
                try
                {
                    parameters.maxNumberOfSteps = int.Parse(formCollection["maxNumberOfSteps"]);
                }
                catch
                {
                    parameters.maxNumberOfSteps = 50;
                }
                algorithm.SetParameters(parameters);
                HillClimbingSolution hillClimbingSolution = new HillClimbingSolution();
                algorithm.SetSolution(hillClimbingSolution);
            }
            else if (algorithmSort == "simulatedAnnealing")
            {
                simulatedAnnealingSolution simulatedAnnealingSolution = new simulatedAnnealingSolution();
                SimulatedAnnealingParameters parameters = new SimulatedAnnealingParameters();
                try
                {
                    parameters.coolingFactor = int.Parse(formCollection["coolingFactor"]);
                }
                catch
                {
                    parameters.coolingFactor = 1;
                }

                try
                {
                    parameters.startingTemperature = int.Parse(formCollection["startingTemperature"]);
                }
                catch
                {
                    parameters.startingTemperature = 10000;
                }

                algorithm.SetSolution(simulatedAnnealingSolution);
                algorithm.SetParameters(parameters);
            }
            else if (algorithmSort == "localBeamSearch")
            {
                localBeamSearchParameters parameters = new localBeamSearchParameters();
                try
                {
                    parameters.numberOfStates = int.Parse(formCollection["numberOfStates"]);
                }
                catch
                {
                    parameters.numberOfStates = 3;
                }

                try
                {
                    parameters.maxNumberOfSteps = int.Parse(formCollection["maxNumberOfSteps"]);
                }
                catch
                {
                    parameters.maxNumberOfSteps = 50;
                }

                algorithm.SetParameters(parameters);
                localBeamSearchSolution localBeamSearchSolution = new localBeamSearchSolution();
                algorithm.SetSolution(localBeamSearchSolution);
            }
            else  // geneticAlgorithms
            {
                GeneticAlgorithmParameters parameters = new GeneticAlgorithmParameters();
                try
                {
                    parameters.sizeOfASingleGeneration = int.Parse(formCollection["sizeOfASingleGeneration"]);
                }
                catch
                {
                    parameters.sizeOfASingleGeneration = 100;
                }
                try
                {
                    parameters.percentOfElitism = int.Parse(formCollection["percentOfElitism"]);
                }
                catch
                {
                    parameters.percentOfElitism = 20;
                }
                try
                {
                    parameters.crossoverProbability = int.Parse(formCollection["crossoverProbability"]);
                }
                catch
                {
                    parameters.crossoverProbability = 35;
                }
                try
                {
                    parameters.mutationProbability = int.Parse(formCollection["mutationProbability"]);
                }
                catch
                {
                    parameters.mutationProbability = 5;
                }
                try
                {
                    parameters.numberOfGenerations = int.Parse(formCollection["numberOfGenerations"]);
                }
                catch
                {
                    parameters.numberOfGenerations = 1000;
                }




               

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
            chessboard.RandomizeChessboard();


            Algorithm algorithm = Algorithm.Instance;
            algorithm.SetChessboard(chessboard);


            return View("index", algorithm); ;
        }


        public IActionResult displayChessboard()
        {
            Chessboard chessboard = new Chessboard(9);
            return View(chessboard);
        }

    }
}