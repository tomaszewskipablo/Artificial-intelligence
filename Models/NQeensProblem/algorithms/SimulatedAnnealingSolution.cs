using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class simulatedAnnealingSolution : ISolution
    {
        public Chessboard solve(Chessboard board, IParams iparams)
        {
            SimulatedAnnealingParameters parameters = new SimulatedAnnealingParameters();

            int[] inputArray = new int[board.size];
            do
            {                                
                board.board.CopyTo(inputArray, 0);

                int heuristicPrev = board.Heuristic();

                board.MoveRandomlyOneQueen();

                int heuristicAfter = board.Heuristic();
                if (heuristicAfter == 0)
                    return board;

                if (heuristicAfter >= heuristicPrev)
                {
                    inputArray.CopyTo(board.board, 0);
                }
            } while (true);






            return board;
        }


    }
}
