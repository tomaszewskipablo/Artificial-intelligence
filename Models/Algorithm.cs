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

        public void SetSolution(ISolution solution)
        {
            this.solution = solution;
        }
    }
}
