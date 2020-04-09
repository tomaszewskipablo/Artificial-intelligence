using ArtificialIntelligence.Models.NQeensProblem;
using ArtificialIntelligence.Models.NQeensProblem.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    // 1. Generate X random states and save them in states list.
    // 2. Find best state in list(the lowest heuristic result).
    // 3 .If best result == 0: return best state.
    // 4. Move queen vertically in every column and count heuristic
    // 5. Save queen on place where heuristic is the best.
    // 6. Move to next column and repeat 4, 5, 6 steps untill all columns visited
    // 7. If board is blocked  -> generate new board
    // 8. Reapet 4-8 untill every state in list visited
    // 9. Repeat steps 2-9 until (steps = maximum number of steps) or (heuristic = 0) 
    public class localBeamSearchSolution : ISolution
    {
        localBeamSearchParameters parameters;
        int indexOfBest;
        int heuristicOfBest;
        public Chessboard solve(Chessboard board, IParams iparams)
        {
            parameters = (localBeamSearchParameters)iparams;
            board.steps = 0;

            // Create and generate numberOfStates element in List
            List<Chessboard> States = new List<Chessboard>();
            GenerateList(States, board.size);


            do
            {
                board.steps++;

                // Find best state in the list(the lowest heuristic result).
                indexOfBest = IndexOfBestHeuristic(States);
                heuristicOfBest = States[indexOfBest].Heuristic();

                // check if finish 
                if (heuristicOfBest == 0 || parameters.maxNumberOfSteps <= board.steps)
                {
                    // save state with best heuristic as a result
                    board.board = States[indexOfBest].board;

                    // if solved
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
                        // move pice to top spot of the column(0)
                        States[s].board[i] = 0;
                        // count heuristic for every position in coumn and save it in heuristic array
                        for (int j = 0; j < board.size; j++)
                        {
                            heuristic[j] = States[s].Heuristic();
                            // move to row in column
                            States[s].board[i] = j + 1;
                        }
                        // move pice to place with best heuristic
                        SetPiceToMinHeuristic(heuristic, States[s].board, i);
                    }
                    // if array is equal to input array -> stuck, randomize new array
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
        private void GenerateList(List<Chessboard> List, int sizeOfBoard)
        {
            for (int i = 0; i < parameters.numberOfStates; i++)
            {
                List.Add(new Chessboard(sizeOfBoard));
            }
        }
        private int IndexOfBestHeuristic(List<Chessboard> States)
        {
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
            return indexOfBest;
        }
    }
}
