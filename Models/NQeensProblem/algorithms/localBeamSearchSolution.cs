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
            board.steps = 0;
            List<Chessboard> States = new List<Chessboard>();

            for (int i = 0; i < parameters.numberOfStates; i++)
            {
                States.Add(new Chessboard(board.size));
            }

            do
            {
                board.steps++;
                int indexOfBest = 0;
                int heuristicOfBest = States[0].Heuristic();
                for (int i = 1; i < parameters.numberOfStates; i++)
                {
                    int iHeuristic = States[i].Heuristic();
                    if (iHeuristic < heuristicOfBest)
                    {
                        indexOfBest = i;
                        heuristicOfBest = iHeuristic;
                    }
                }
                if (heuristicOfBest == 0 || parameters.maxNumberOfSteps <= board.steps)
                {
                    board.board = States[indexOfBest].board;
                    
                    if (heuristicOfBest == 0)
                    {                       
                        board.isSolved = true;
                        board.finalHeuristic = board.Heuristic();
                    }
                    
                    return board;
                }

                //Move queens in every column on theirs best local position in every state.
                for (int s = 0; s < parameters.numberOfStates; s++)
                {
                    int[] inputArray = new int[board.size];

                    int[] heuristic = new int[board.size];

                    States[s].board.CopyTo(inputArray, 0);
                    for (int i = 0; i < States[s].size; i++)
                    {

                        States[s].board[i] = 0;
                        for (int j = 0; j < board.size; j++)
                        {
                            heuristic[j] = States[s].Heuristic();
                            States[s].board[i] = j + 1;
                        }
                        SetPiceToMinHeuristic(heuristic, States[s].board, i);
                    }
                    if (AreArraysEqual(inputArray, States[s].board))
                    {
                        States[s].RandomizeChessboard();
                    }
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
