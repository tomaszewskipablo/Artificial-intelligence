using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class Chessboard
    {
        int[] board;
        public int QueensOnBoard=0;
        public int size=4;

        public Chessboard(int size)
        {
            this.size = size;
            board = new int[size];

            //// initialize with 0 
            //for (int i=0;i<size;i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        borad[i] = false;
            //    }
            //}
        }
        public void randomizeChessboard()
        {
            Random rnd = new Random();

            board = new int[size];
            

            for (int i = 0;i<size;i++)
            {
                int random = rnd.Next(0, size);
                board[i] = random;
            }
            QueensOnBoard = 1;
        }

        public bool isQueenOnBox(int i, int j)
        {
            return board[j] == i;
        }
    }
}
