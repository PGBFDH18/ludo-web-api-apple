using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;

namespace GameEngine
{
    /// <summary>
    /// Store game state on database.
    /// </summary>
    public class Database : ILoadSave
    {
        public string Source { get; set; } // <- Connection string.

        public Database(string connection)
        {
            Source = connection;
        }

        public void Save(Game game)
        {
            throw new NotImplementedException();
        }

        public Game Load(int gameID)
        {
            throw new NotImplementedException();
        }

        public List<Game> Load()
        {
            throw new NotImplementedException();
        }

        public void Delete(int gameID)
        {
            throw new NotImplementedException();
        }

        public void Update(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
