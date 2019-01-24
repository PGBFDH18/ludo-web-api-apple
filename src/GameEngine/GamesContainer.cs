using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameEngine;

namespace WebApi
{
    // Testklass (än så länge).
    public class GamesContainer : ILoadSave
    {
        public string Source { get; set; } // Ingen betydelse.

        private static List<Game> games = new List<Game>();

        public void Save(Game game)
        {
            games.Add(game);
        }

        public Game Load(int gameID)
        {
            var resultGame = 
                from game in games
                where game.GameID == gameID
                select game;

            return resultGame.First(); 
        }

        public List<Game> Load() => games;
    }
}
