using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum Sign
{
    empty,
    cross,
    circle
}


namespace ArtificialIntelligence.Models.MinMax
{
    public class GameState
    {
        private static GameState instance = null;
        private GameState() { }

        public static GameState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameState();
                }
                return instance;
            }
        }

        private Player[] player;

        private Player PlayernTurn;

        private Sign[,] board = new Sign[3,3];


        public void MakeMove(int x, int y)
        {
            // TODO, insted of if, disable button in index
            if(board[x,y]==Sign.empty)
            {
                board[x, y] = PlayernTurn.GetSign();
            }
        }
    }
}
