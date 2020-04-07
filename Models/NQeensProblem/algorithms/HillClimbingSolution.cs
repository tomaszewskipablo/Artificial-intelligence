using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class HillClimbingSolution : ISolution
    {
        public Chessboard solve(Chessboard board, IParams iparams)
        {
            HillClimbingParameters parameters = (HillClimbingParameters)iparams;
            
            
            int[] inputArray = new int[board.size];
            
            int[] heuristic = new int[board.size];
            int step = 0;
            
            do
            {
                step++;
                board.board.CopyTo(inputArray, 0);
                for (int i = 0; i < board.size; i++)
                {
                    
                    board.board[i] = 0;
                    for (int j = 0; j < board.size; j++)
                    {
                        heuristic[j] = board.Heuristic();
                        board.board[i] = j+1;
                    }
                    SetPiceToMinHeuristic(heuristic, board.board, i);
                }

                if(board.Heuristic() == 0 || parameters.maxNumberOfSteps <= step)
                { 
                     return board;
                }
                if(AreArraysEqual(inputArray, board.board))
                {
                    board.RandomizeChessboard();
                }    

            } while (true);
        }
        private void SetPiceToMinHeuristic(int[] heuristic, int[] board, int i)
        {
            // min value
            int m = heuristic.Min();

            // Positioning min
            int p = Array.IndexOf(heuristic, m);

            board[i] = p;
        }
        private bool AreArraysEqual(int[] a, int[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
    }
}
  
