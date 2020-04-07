using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class localBeamSearchSolution : ISolution
    {
        public Chessboard solve(Chessboard board, IParams iparams)
        {
            localBeamSearchParameters parameters = (localBeamSearchParameters)iparams;

            List<Chessboard> States = new List<Chessboard>();

            for (int i = 0; i < parameters.numberOfStates; i++)
            {
                States.Add(new Chessboard(board.size));
            }


            Chessboard chessboard = board;


            return chessboard;
        }
    }
}
