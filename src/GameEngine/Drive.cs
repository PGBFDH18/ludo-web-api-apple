using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    /// <summary>
    /// Store game state on local drive.
    /// </summary>
    public class Drive : ILoadSave
    {
        public string SavePath { get; set; }
        public string LoadPath { get; set; }

        public void Save(Game game)
        {
            throw new NotImplementedException();
        }

        public Game Load(int gameID)
        {
            throw new NotImplementedException();
        }
    }
}
