using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ArtificialIntelligence.Models
{
    public class Chessboard
    {
        public int[] board;
        public int QueensOnBoard = 0;
        public int size = 4;
        public bool isSolved;

        public Chessboard(int size)
        {
            this.size = size;
            board = new int[size];

            RandomizeChessboard();
        }
        public void RandomizeChessboard()
        {
            Random rnd = new Random();

            board = new int[size];


            for (int i = 0; i < size; i++)
            {
                int random = rnd.Next(0, size);
                board[i] = random;
            }
            QueensOnBoard = 1;
        }

        public bool IsQueenOnBox(int i, int j)
        {
            return board[j] == i;
        }

        public int Heuristic()
        {

            int heuristic = 0;
            for (int i = 0; i < size; i++)
            {

                for (int j = i + 1; j < size; j++)
                {
                    //hirizontaly
                    if (board[i] == board[j])
                    {
                        heuristic++;
                    }

                    // awry (right-bottom)
                    if (board[i] + j - i == board[j])
                    {
                        heuristic++;
                    }

                    //awry(right - up)
                    if (board[i] + i - j == board[j])
                    {
                        heuristic++;
                    }
                }
            }
            return heuristic;
        }


        public int GetRandomQeenOnBoard()
        {
            Random rnd = new Random();

            return rnd.Next(0, size);
        }


        public int GetRandomCordinateOnBoard()
        {
            Random rnd = new Random();
            return rnd.Next(0, size);
        }
        public void MoveRandomlyOneQueen()
        {
            int queen = GetRandomQeenOnBoard();

            int newPlace = GetRandomCordinateOnBoard();

            board[queen] = newPlace;
        }

    }
}
