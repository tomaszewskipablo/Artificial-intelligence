using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        // enums
    }
}
