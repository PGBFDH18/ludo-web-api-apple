using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface IGamesContainer
    {
        void Add(Game game);
        Game Load(int gameID);
        List<Game> Load();
        void Delete(int gameID);
    }
}
