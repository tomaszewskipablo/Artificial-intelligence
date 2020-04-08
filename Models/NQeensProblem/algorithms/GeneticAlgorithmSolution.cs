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
        public Chessboard solve(Chessboard board, IParams iparams)
        {
            GeneticAlgorithmParameters parameters = (GeneticAlgorithmParameters)iparams;
            board.steps = 0;
            
          
            List<Chessboard> Generation = new List<Chessboard>();

            generateGenereation(Generation, parameters.numberOfGenerations, board.size);

            Sort(Generation);

            

            if (Generation[0].finalHeuristic == 0)
            {
                board.board = Generation[0].board;
                board.isSolved = true;
                board.finalHeuristic = board.Heuristic();               
            }

            return Generation[0];
        }
        private void generateGenereation(List<Chessboard> Generation, int sizeOfGeneration, int sizeOfBoard)
        {
            for (int i = 0; i < sizeOfGeneration; i++)
            {
                Generation.Add(new Chessboard(sizeOfBoard));
            }
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

    }

}