using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtificialIntelligence.Models.MinMax
{
    public class Player
    {
        private int wins;
        private bool isCross;

        public Player(bool isCross)
        {
            this.isCross = isCross;
        }
        public void AddWin()
        {
            wins++;
        }
        public int GetWin()
        {
            return wins;
        }
        public bool GetIsCross()
        {
            return isCross;
        }
    }
}
