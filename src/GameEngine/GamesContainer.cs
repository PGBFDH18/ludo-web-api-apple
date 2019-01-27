using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameEngine;

namespace GameEngine
{
    public class GamesContainer : IGamesContainer
    {
        private static List<Game> games = new List<Game>();

        public void Add(Game game) => games.Add(game);

        public Game Load(int gameID) => games.Where(g => g.ID == gameID).First();

        public List<Game> Load() => games;
    }
}
