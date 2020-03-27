using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class Algorithm
    {
        private ISolution solution;
        private Chessboard chessboard;

        // 5 variables
        public int param1;
        public int param2;
        public int param3;
        public int param4;
        public int param5;

        public void SetSolution(ISolution solution)
        {
            this.solution = solution;
        }

        public void SetChessboard(Chessboard chessboard)
        {
            this.chessboard = chessboard;
        }
        public Chessboard GetChessboard()
        {
            return chessboard;
        }

        public Chessboard DoAlgorithm()
        {
            return (solution.solve(param1, param2, param3, param4, param5, chessboard));
        }
    }
}
