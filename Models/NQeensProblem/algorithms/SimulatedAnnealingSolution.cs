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

        //   1.Move 1 queen randomly
        //   2. If heuristic result of state after move is worse or equal:
        //         a) Calculate probability of acceptance:  e^(h/T)
        //              T = Temperature
        //              h = current heuristic result - new heuristic result
        //          b) Generate random value from 0 to 1.
        //          c) If value > probabiltyOfacceptance  come back to input state
        //   4. Reduce Temperature by Cooling Factor
        //   
        Reduce Temperature by CoolingFactor.
    If board after move is solved (heuristic result == 0) return solved board.
    Repeat steps 1-6 until Temperature is bigger than 0.


        public Chessboard solve(Chessboard board, IParams iparams)
        {
            SimulatedAnnealingParameters parameters = (SimulatedAnnealingParameters)iparams;
            board.steps = 0;

            // needed when random move won't be save
            int[] inputArray = new int[board.size];
            int heuristicAfter;
            do
            {
                board.steps++;
                board.board.CopyTo(inputArray, 0);

                int heuristicPrev = board.Heuristic();

                board.MoveRandomlyOneQueen();

                heuristicAfter = board.Heuristic();
                

                if (heuristicAfter >= heuristicPrev)
                {
                    int h = heuristicAfter - heuristicPrev;
                    double T = parameters.startingTemperature;
                    double probabiltyOfacceptance = Math.Exp(h / T);

                    double random = GenerateRandomVaule();

                    // if bigger come back to previosu state
                    if(random > probabiltyOfacceptance)
                    {
                        inputArray.CopyTo(board.board, 0);
                    }
                    parameters.startingTemperature -= parameters.coolingFactor;
                }

                  
            } while (heuristicAfter != 0 && parameters.startingTemperature > 0);

            if(heuristicAfter==0)
            {
                board.isSolved = true;
            }
            else
            {
                board.isSolved = false;
            }
            board.finalHeuristic = board.Heuristic();
            return board;
        }
        public double GenerateRandomVaule()
        {
            Random rnd = new Random();
            int random = rnd.Next(0, 100);
            return (double)random / 100.0;
        }

    }
}
