using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class Game
    {
        // Save och Load funktionalitet.
        public IProvider Database { get; set; }

        // Deltagarna ligger här!
        public List<Player> Players { get; set; }

        public int GameID { get; set; }

        public Game(List<Player> players, string databaseConnectionString)
        {
            Players = players;
            Database = new Database(databaseConnectionString);
        }
    }
}
