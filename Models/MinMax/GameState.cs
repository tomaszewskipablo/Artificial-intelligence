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

        private bool IsAImove=true;

        private Sign[,] board = new Sign[3,3];

        public GameStatus gameStatus = GameStatus.notFinished;

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
            Random rnd = new Random();

            Field field;
            do
            {                
                field.x =  rnd.Next(0, 3);
                field.y = rnd.Next(0, 3);
            } while (isCircle(field) || isCross(field));

            return field;
        }

        public void StartGame()
        {
            player = new Player[2] { new Player(), new Player() };
            player[1].isAI = true;            
            player[0].SetSign(Sign.cross);
            player[1].SetSign(Sign.circle);

            currenPlayer = player[1];
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
        public bool CheckGameStatus()
        {
            if(moveCount==9)
            {
                gameStatus = GameStatus.tie;
                return true;
            }
            


            
            for (int i = 0; i < 2; i++)
            {
                Sign sign = player[i].GetSign();
                //check col
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (board[x, y] != sign)
                            break;
                        if (x == 2)
                        {
                            if (player[i].isAI)
                            {
                                gameStatus = GameStatus.bootWon;
                            }
                            else
                                gameStatus = GameStatus.playerWon;

                            return true;
                        }
                    }
                }


                ////check row
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (board[x, y] != sign)
                            break;
                        if (y == 2)
                        {
                            if (player[i].isAI)
                            {
                                gameStatus = GameStatus.bootWon;
                            }
                            else
                                gameStatus = GameStatus.playerWon;

                            return true;
                        }
                    }
                }

                ////check diag
                for (int s = 0; s < 3; s++)
                {
                    {
                        if (board[s, s] != sign)
                            break;
                        if (s == 2)
                        {
                            if (player[i].isAI)
                            {
                                gameStatus = GameStatus.bootWon;
                            }
                            else
                                gameStatus = GameStatus.playerWon;

                            return true;
                        }
                    }
                }

                //check anti-diag
                for (int s = 0; s < 3; s++)
                {
                    {
                        if (board[2 - s, s] != sign)
                            break;
                        if (s == 2)
                        {
                            if (player[i].isAI)
                            {
                                gameStatus = GameStatus.bootWon;
                            }
                            else
                                gameStatus = GameStatus.playerWon;

                            return true;
                        }
                    }
                }

                ////check draw
                //if (moveCount == (Math.pow(n, 2) - 1))
                //{
                //    //report draw
                //}
            }
            return false;
        }
        public void RestartGame()
        {
            for(int i = 0; i<3;i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i,j] = Sign.empty;
                }
            }
            moveCount = 0;
        }
    }
}
