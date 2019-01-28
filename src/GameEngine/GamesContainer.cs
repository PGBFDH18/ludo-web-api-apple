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

        public void Add(Game game)
        {
            // Bug: Only one game can be present in memory at a time.
            var gameIds = new List<int>();
            try
            {
                gameIds = Game.LoadSaveService.Load().Select(g => g.ID).ToList();
                game.ID = gameIds.Max(id => id) + 1;
            }
            catch (Exception)
            {
                gameIds.Add(0);
            }
            games.Add(game);
        }

        public void Delete(int gameID)
        {
            Game gameToRemove = games.Where(g => g.ID == gameID).First();
            games.Remove(gameToRemove);
        }

        public Game Load(int gameID) => games.Where(g => g.ID == gameID).First();

        public List<Game> Load() => games;
    }
}
