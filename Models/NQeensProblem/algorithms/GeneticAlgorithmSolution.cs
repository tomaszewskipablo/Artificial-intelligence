using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    // 1. Generate X random states and save them in states list.
    // 2. If one of the state in generation list is solved: return solved state.
//    Create newGeneration list (there new chromosomes will be saved).
//    Sort generation list(states with the lowest heuristic result at the beginning).
//    Save X first states into elite list(list with best results).
//    Save elite into newGeneration.
//    While newGeneration chromosomes count is not equal SizeOfSingleGeneration repeat:

//a) Selection: Randomly select 2 chromosome parents from generation list.

//b) Crossover: Randomly select a point in the state and exchange all parent columns beyond that point.

//c) Mutation: Move a queen from random column onto random row.

//d) Add 2 chromosomes into newGeneration list.

    //generation is changed into newGeneration.
    //Repeat NumberOfGenerations times steps 2-
    public class GeneticAlgorithmSolution : ISolution
    {
        public int heuristicSum = 0;
        public int GenerationNumber=1;
        public double sum = 0;
        GeneticAlgorithmParameters parameters;

        public Chessboard solve(Chessboard board, IParams iparams)
        {
            parameters = (GeneticAlgorithmParameters)iparams;
            


            List<Chessboard> Generation = new List<Chessboard>();
            List<Chessboard> NewGeneration = new List<Chessboard>();

            GenerateGenereation(Generation, parameters.sizeOfASingleGeneration, board.size);

            while (GenerationNumber < parameters.numberOfGenerations)
            {
                CountHeuristicOfWholePopulation(Generation);
                Sort(Generation);

                //check if solved
                if (Generation[0].finalHeuristic == 0)
                {
                    board.board = Generation[0].board;
                    board.steps = GenerationNumber;
                    board.isSolved = true;
                    board.finalHeuristic = board.Heuristic();
                    return board;
                }


                Elitism(Generation, NewGeneration);

                while (NewGeneration.Count != Generation.Count)
                {                 
                    // --------------- Selection ------------------
                    Chessboard parentA = SelectParentTournamet(Generation);

                    Chessboard parentB = SelectParentTournamet(Generation);

                    // --------------- Crossover -------------
                    Crossover(parentA, parentB, NewGeneration);

                    // --------------- Mutate ----------------
                    Mutate(NewGeneration);
                }

                GenerationNumber++;
                NewGenerationBecomesParentsGeneration(Generation, NewGeneration);
            }
            board.steps = parameters.numberOfGenerations;
            return Generation[0];
        }
        private void Mutate(List<Chessboard> NewGeneration)
        {
            Random rnd = new Random();
            // if probability ok -> mutate
            if (rnd.Next(0, 100) < parameters.mutationProbability)
            {
                // take last element in NewGeneration (newly added child) and mutate
                NewGeneration[NewGeneration.Count-1].MoveRandomlyOneQueen();
            }
            if (rnd.Next(0, 100) < parameters.mutationProbability)
            {
                // take one before last element in NewGeneration (second newly added child) and mutate
                NewGeneration[NewGeneration.Count - 2].MoveRandomlyOneQueen();
            }
        }
        
        // copy NewGeneration elements to Genration List
        private void NewGenerationBecomesParentsGeneration(List<Chessboard> Generation, List<Chessboard> NewGeneration)
        {
            Generation.Clear();
            for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            {
                Generation.Add(NewGeneration[i]);
            }
            NewGeneration.Clear();
        }
        private void GenerateGenereation(List<Chessboard> Generation, int sizeOfGeneration, int sizeOfBoard)
        {
            for (int i = 0; i < sizeOfGeneration; i++)
            {
                Generation.Add(new Chessboard(sizeOfBoard));
            }
        }
        // copy eliete elements (percentOfElitism) from Generation to NewGenration List
        private void Elitism(List<Chessboard> Generation, List<Chessboard> NewGeneration)
        {
            int range = parameters.percentOfElitism;
            if (parameters.percentOfElitism > parameters.sizeOfASingleGeneration)
            {
                range = parameters.sizeOfASingleGeneration;
            }
            for (int i = 0; i < range; i++)
            {
                NewGeneration.Add(Generation[i]);
            }
        }


        private Chessboard SelectParent(List<Chessboard> Generation)
        {
            Random rnd = new Random();
            int random = rnd.Next(0, (int)sum);

            double temp = 0;

            for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            {
                temp += heuristicSum / Generation[i].finalHeuristic;
                if (random < temp)
                {
                    return Generation[i];
                }

            }
            return Generation[0];
        }
        private Chessboard SelectParentTournamet(List<Chessboard> Generation)
        {            
            List<Chessboard> Tournament = new List<Chessboard>();
            Random rnd = new Random();
            // randomly choose 10% of sizeOfASingleGeneration 
            for (int i = 0; i < 10; i++)
            {
                int random = rnd.Next(0, parameters.sizeOfASingleGeneration/10);
                Tournament.Add(Generation[random]);
            }
            Sort(Tournament);
            
            // take the best one
            return Tournament[0];
        }


        //single-point crossover: randomly select a point in chromosome code and exchange all parent genes beyond that point
        private void Crossover(Chessboard parentA, Chessboard parentB, List<Chessboard> NewGeneration)
        {
            Chessboard childA = new Chessboard(parentA.size);
            Chessboard childB = new Chessboard(parentA.size);            
                        
            Random rnd = new Random();
            // if crossover probability done -> cross  
            if (rnd.Next(0, 100) < parameters.crossoverProbability)
            {
                int split = rnd.Next(0, parentA.size);

                for (int i = 0; i < split; i++)
                {
                    childA.board[i] = parentA.board[i];
                }
                for (int i = split; i < parentA.size; i++)
                {
                    childA.board[i] = parentB.board[i];
                }
            }
            // if not, copy
            else
            {
                for (int i = 0; i < parentA.size; i++)
                {
                    childA.board[i] = parentA.board[i];
                    childB.board[i] = parentB.board[i];
                }
            }
            NewGeneration.Add(parentA);
            NewGeneration.Add(parentB);
        }
        // BUBLE SORT
        private void Sort(List<Chessboard> Generation)
        {            
            Chessboard temp;
            for (int j = 0; j <= Generation.Count - 2; j++)
            {
                for (int i = 0; i <= Generation.Count - 2; i++)
                {
                    if (Generation[i].finalHeuristic > Generation[i + 1].finalHeuristic)
                    {
                        temp = Generation[i + 1];
                        Generation[i + 1] = Generation[i];
                        Generation[i] = temp;
                    }
                }
            }
        }
        private void CountHeuristicOfWholePopulation(List<Chessboard> Generation)
        {
            for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            {
                Generation[i].finalHeuristic = Generation[i].Heuristic();
            }

            //heuristicSum = 0;
            //for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            //{
            //    heuristicSum += Generation[i].finalHeuristic;
            //}
            //for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            //{
            //    sum += (double)heuristicSum / Generation[i].finalHeuristic;
            //}
        }

    }

}