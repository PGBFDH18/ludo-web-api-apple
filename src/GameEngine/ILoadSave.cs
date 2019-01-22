using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface ILoadSave
    {
        void Save(Game game);
        Game Load(int gameID);
    }
}
