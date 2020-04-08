using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class GeneticAlgorithmSolution : ISolution
    {
        public int heuristicSum=0;
        public double sum = 0;
        GeneticAlgorithmParameters parameters;

        public Chessboard solve(Chessboard board, IParams iparams)
        {
            parameters = (GeneticAlgorithmParameters)iparams;
            board.steps = 0;
            
          
            List<Chessboard> Generation = new List<Chessboard>();
            List<Chessboard> NewGeneration = new List<Chessboard>();

            GenerateGenereation(Generation, parameters.sizeOfASingleGeneration, board.size);
            
            Sort(Generation);


            //check if solved
            if (Generation[0].finalHeuristic == 0)
            {
                board.board = Generation[0].board;
                board.isSolved = true;
                board.finalHeuristic = board.Heuristic();
                return board;
            }

            //NewGeneration.Add()

            //
            CountHeuristicOfWholePopulation(Generation);

            while (NewGeneration.Count != Generation.Count)
            {
                Chessboard child = new Chessboard(board.size);
                
                //selection
                Chessboard parentA = SelectParent(Generation);
                Chessboard paerntB = SelectParent(Generation);

                //crossover


                // mutate

                NewGeneration.Add(child);


            }

            return Generation[0];
        }
        private void GenerateGenereation(List<Chessboard> Generation, int sizeOfGeneration, int sizeOfBoard)
        {
            for (int i = 0; i < sizeOfGeneration; i++)
            {
                Generation.Add(new Chessboard(sizeOfBoard));
            }
        }
        private Chessboard SelectParent(List<Chessboard> Generation) 
        {
            Random rnd = new Random();
            int random = rnd.Next(0, (int)sum);


            for(int i=0;i< parameters.sizeOfASingleGeneration; i++)
            {
                if(random > heuristicSum)
                {
                    return Generation[i];
                }
                heuristicSum += Generation[i].finalHeuristic/ heuristicSum;
            }
            return Generation[0];      
        }

        private void Sort(List<Chessboard> Generation)
        {
            // BUBLE SORT
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
            heuristicSum = 0;
            for (int i=0;i<parameters.sizeOfASingleGeneration; i++)
            {
                heuristicSum += Generation[i].finalHeuristic;                
            }
            for (int i = 0; i < parameters.sizeOfASingleGeneration; i++)
            {
                sum += (double)heuristicSum / Generation[i].finalHeuristic;
            }
        }

    }

}