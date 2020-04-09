﻿using ArtificialIntelligence.Models.NQeensProblem;
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
            board.steps=0;

            int[] inputArray = new int[board.size];

            int[] heuristic = new int[board.size];
            

            do
            {
                board.steps++;

                board.board.CopyTo(inputArray, 0);
                for (int i = 0; i < board.size; i++)
                {
                    // move pice to top spot of the column(0)
                    board.board[i] = 0;
                    // count heuristic for every position in coumn and save it in heuristic array
                    for (int j = 0; j < board.size; j++)
                    {
                        heuristic[j] = board.Heuristic();
                        // move to row in column
                        board.board[i] = j + 1;
                    }
                    // move pice to place with best heuristic
                    SetPiceToMinHeuristic(heuristic, board.board, i);
                }
                
                // Check if solved
                if (board.Heuristic() == 0 || parameters.maxNumberOfSteps <= board.steps)
                {
                    if(board.Heuristic() == 0)
                    {
                        board.isSolved = true;
                    }
                    board.finalHeuristic = board.Heuristic();
                    return board;
                }
                // if array is equal to input array -> suck, randomize new array
                if (AreArraysEqual(inputArray, board.board))
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
            // compare every element in arrays
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                    return false;
            }
            return true;
        }
    }
}

