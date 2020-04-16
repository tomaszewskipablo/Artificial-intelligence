using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models
{
    public class Player
    {
        private int wins;
        private Sign sign;
        private bool isPlayerTurn;
        public bool isAI;
        public Player()
        {
        }

        public Player(Sign sign)
        {
            this.sign = sign;
        }
        public void AddWin()
        {
            wins++;
        }
        public int GetWin()
        {
            return wins;
        }
        public Sign GetSign()
        {
            return sign;
        }
        public void SetSign(Sign sign)
        {
            this.sign = sign;
        }
        public bool GetIsPlayerTurn()
        {
            return isPlayerTurn;
        }
    }
}
