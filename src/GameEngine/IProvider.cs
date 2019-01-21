using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public interface IProvider
    {
        void Save(Game game);
        Game Load(int gameID);
    }
}
