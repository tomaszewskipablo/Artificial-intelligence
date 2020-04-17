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

public enum GameStatus
{
    notFinished,
    tie,
    playerWon,
    bootWon,
}


namespace ArtificialIntelligence.Models
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

        private Player currenPlayer;

        private bool IsAImove=false;

        private Sign[,] board = new Sign[3,3];

        private GameStatus gameStatus = GameStatus.notFinished;

        private int moveCount=0;

        public void SetCurrentPlayer(Player player)
        {
            this.currenPlayer = player;
        }
        public bool GetIsAImove()
        {
            return IsAImove;    
        }
        public void SetIsAImove(bool isAiMove)
        {
            this.IsAImove = isAiMove;
        }
        public Field AIMove()
        {
            return new Field(1,1);
        }

        public void StartGame()
        {
            player = new Player[2] { new Player(), new Player() };
            player[0].SetSign(Sign.cross);
            player[1].SetSign(Sign.circle);
            currenPlayer = player[0];
        }
        public void MakeMove(Field field)
        {
            // TODO, insted of if, disable button in index
            if(board[field.x, field.y]==Sign.empty)
            {
                board[field.x, field.y] = currenPlayer.GetSign();
                moveCount++;
                IsAImove = !IsAImove;
                if(currenPlayer == player[1]) 
                {
                    currenPlayer = player[0];
                }
                else
                    currenPlayer = player[1];
            }
            
        }
        public bool isCross(Field field)
        {
            return board[field.x, field.y] == Sign.cross;
        }
        public bool isCircle(Field field)
        {
            return board[field.x, field.y] == Sign.circle;
        }
        public void CheckWin()
        {
            ////check col
            //for (int i = 0; i < 3; i++)
            //{
            //    if (board[x][y] != s)
            //        break;
            //    if (i == n - 1)
            //    {
            //        //report win for s
            //    }
            //}

            ////check row
            //for (int i = 0; i < n; i++)
            //{
            //    if (board[i][y] != s)
            //        break;
            //    if (i == n - 1)
            //    {
            //        //report win for s
            //    }
            //}

            ////check diag
            //if (x == y)
            //{
            //    //we're on a diagonal
            //    for (int i = 0; i < n; i++)
            //    {
            //        if (board[i][i] != s)
            //            break;
            //        if (i == n - 1)
            //        {
            //            //report win for s
            //        }
            //    }
            //}

            ////check anti diag (thanks rampion)
            //if (x + y == n - 1)
            //{
            //    for (int i = 0; i < n; i++)
            //    {
            //        if (board[i][(n - 1) - i] != s)
            //            break;
            //        if (i == n - 1)
            //        {
            //            //report win for s
            //        }
            //    }
            //}

            ////check draw
            //if (moveCount == (Math.pow(n, 2) - 1))
            //{
            //    //report draw
            //}
        }
    }
}
