using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameEngine;

namespace WebApi
{
    // Testklass (än så länge).
    public static class GamesContainer
    {
        private static List<Game> games = new List<Game>();

        public static void Add(Game game)
        {
            games.Add(game);
        }

        public static Game GetGame(int gameID)
        {
            var resultGame = 
                from game in games
                where game.ID == gameID
                select game;

            return resultGame.First(); 
        }

        public static List<Game> Load() => games;

        public static void Delete(int gameID)
        {
            games.Remove(games.Where(g => g.ID == gameID).First());
        }

        public static void DeleteAll()
        {
            games.Clear();
        }
    }
}
