using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameEngine;

namespace WebApi
{
    public static class GamesContainer
    {
        private static ILoadSave loadSaveService;
        private static List<Game> games = new List<Game>();

        // Vilken spara/ladda-service som ska användas bestäms i Program.Main().
        public static ILoadSave LoadSaveService { set => loadSaveService = value; }

        // Minne.
        public static void WriteToMemory(Game game) => games.Add(game);

        public static Game ReadFromMemory(int gameID) => games.Where(g => g.ID == gameID).First();

        public static List<Game> ReadFromMemory() => games;

        public static void Delete(int gameID) => games.Remove(games.Where(g => g.ID == gameID).First());

        public static void DeleteAll() => games.Clear();

        // Lagring.
        public static void WriteToLoadSaveService(Game game) => loadSaveService.Save(game);

        public static Game ReadFromLoadSaveService(int gameID) => loadSaveService.Load(gameID);

        public static List<Game> ReadFromLoadSaveService() => loadSaveService.Load();
    }
}
