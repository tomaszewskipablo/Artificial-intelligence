using ArtificialIntelligence.Models.NQeensProblem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public interface ISolution
    {
        public Chessboard solve(Chessboard board,IParams iparams );
    }
}
