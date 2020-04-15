using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    // 1. Create Generation List and NewGeneration List, Generate X random states for Generation
    // 2. If one of the state in generation list is solved: return solved state.
    // 3. Count heuristic for every state in Generation
    // 3. Sort Generation list
    // 4. (elitism) Save X first states into newGeneration.
    // 5. While newGeneration chromosomes count is smaller than SizeOfSingleGeneration repeat:
    // a) (Selection Tournament) Randomly select 10 states from Generation list and return best one 
    // b) (Crossover) Randomly select a index from which place cut satets array: 
    // childA = left side of ParentA array + right side of ParentB
    // childB = left side of ParentB array + right side of ParentA
    // add children to NewGeneration List
    // c) (Mutation) Take 2 last elemnts of NewGeneration List and  mutete them -> Move random queen to random place on board.
    // 6. NewGeneration becomes Generation
    // 7. Repeat untill NumberOfGenerations is smaller then max numberOfGenerations
    public class GeneticAlgorithmSolution : ISolution
    {
        public int heuristicSum = 0;
        public int GenerationNumber = 1;
        public double sum = 0;
        GeneticAlgorithmParameters parameters;

        public Chessboard solve(Chessboard board, IParams iparams)
        {
            parameters = (GeneticAlgorithmParameters)iparams;

            List<Chessboard> Generation = new List<Chessboard>();
            List<Chessboard> NewGeneration = new List<Chessboard>();

            // Generate X random states for Generation
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
                NewGeneration[NewGeneration.Count - 1].MoveRandomlyOneQueen();
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
                int random = rnd.Next(0, parameters.sizeOfASingleGeneration / 10);
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
            // ------------- single-point crossover ---------------
            //if crossover probability done -> cross
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
            else
            {
                for (int i = 0; i < parentA.size; i++)
                {
                    childA.board[i] = parentA.board[i];
                    childB.board[i] = parentB.board[i];
                }
            }
            // ------------- single-point crossover ---------------

            ////-------------uniform crossover-------------- -
            //for (int i = 0; i < parentA.size; i++)
            //{
            //    childA.board[i] = parentA.board[i];
            //    childB.board[i] = parentB.board[i];
            //}

            //for (int i = 0; i < parentA.size; i++)
            //{
            //    if (rnd.Next(0, 100) < parameters.crossoverProbability)
            //    {
            //        int temp = childA.board[i];
            //        childA.board[i] = parentB.board[i];
            //        childB.board[i] = temp;
            //    }
            //}
            ////-------------uniform crossover-------------- -

            NewGeneration.Add(childA);
            NewGeneration.Add(childB);
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