using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace GameEngine
{
    /// <summary>
    /// Store game state on local drive.
    /// </summary>
    public class Drive : ILoadSave
    {
        public string SavePath { get; set; }
        public string LoadPath { get; set; }

        public Drive(string savePath, string loadPath)
        {
            SavePath = savePath;
            LoadPath = loadPath;
        }

        public void Save(Game game)
        {
            //throw new NotImplementedException();

            string json = JsonConvert.SerializeObject(game);
            File.WriteAllText(SavePath, json);
        }

        public Game Load(int gameID)
        {
            var jsonFromFile = File.ReadAllText(LoadPath);
            Game game = (Game)JsonConvert.DeserializeObject(jsonFromFile);

            return game;
        }
    }
}
