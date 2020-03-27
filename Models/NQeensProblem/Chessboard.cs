using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class Chessboard
    {
        bool [,] borad;
       
        public int size=4;

        public Chessboard(int size)
        {
            this.size = size;
            borad = new bool[size,size];

            // initialize with 0 
            for (int i=0;i<size;i++)
            {
                for (int j = 0; j < size; j++)
                {
                    borad[i,j] = false;
                }
            }
        }
        public void randomizeChessboard()
        {
            Random rnd = new Random();

            int [] indexesOfHetmens = new int[size];

            for (int i = 0; i < size; i++)
            {
                int random = rnd.Next(0, size * size);
                if (!indexesOfHetmensInArray(random, indexesOfHetmens))
                {
                    indexesOfHetmens[i] = random;
                }
                else
                {
                    i--;
                }
            }
            for (int i = 0; i < size; i++)
            {

                borad[indexesOfHetmens[i] / size, indexesOfHetmens[i] % size] = true;
            }

        }
        private bool indexesOfHetmensInArray(int random, int [] indexesOfHetmens)
        {
            for (int i = 0; i < size; i++)
                if (indexesOfHetmens[i] == random)
                    return true;

            return false;
        }

    }
}
