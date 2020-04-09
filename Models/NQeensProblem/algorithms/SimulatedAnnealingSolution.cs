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
        //   5. If (heuristic == 0) or (Temperature < 0) finish and return solved board
        //   6. Repeat 1-6  
 


        public Chessboard solve(Chessboard board, IParams iparams)
        {
            SimulatedAnnealingParameters parameters = (SimulatedAnnealingParameters)iparams;
            board.steps = 0;

            // needed when random move won't be save
            int[] inputArray = new int[board.size];
            // needed for checking if new heuristic is better then one before move
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

                    // Calculate probability of acceptance:  e^(h/T)
                    double probabiltyOfacceptance = Math.Exp(h / T);


                    double random = GenerateRandomVaule();
                    // if bigger come back to previosu state
                    if(random > probabiltyOfacceptance)
                    {
                        inputArray.CopyTo(board.board, 0);
                    }                
                }
                // Reduce Temperature by Cooling Factor
                parameters.startingTemperature -= parameters.coolingFactor;
                // If(heuristic == 0) or(Temperature < 0) finish and return solved board
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
