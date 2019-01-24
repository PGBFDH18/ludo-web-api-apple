using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface ILoadSave
    {
        string Source { get; set; }

        void Save(Game game);

        Game Load(int gameID);

        List<Game> Load();
    }
}
