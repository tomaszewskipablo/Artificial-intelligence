using ArtificialIntelligence.Models.NQeensProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class Algorithm
    {
        private static Algorithm instance = null;
        private Algorithm() { }

        public static Algorithm Instance 
        {
            get
            {
                if(instance==null)
                {
                    instance = new Algorithm();
                }
                return instance;
            }
        }


        private ISolution solution;
        public  Chessboard chessboard;

        private IParams parameters;

        public void SetSolution(ISolution solution)
        {
            this.solution = solution;
        }

        public void SetParameters(IParams parameters)
        {
            this.parameters = parameters;
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
            return (solution.solve(chessboard, parameters));
        }
    }
}
