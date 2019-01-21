using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace GameEngine
{
    /// <summary>
    /// Store game state on database.
    /// </summary>
    public class Database : IProvider
    {
        public SqlConnection Connection { get; set; }
        
        // Om man vill skicka SqlConnection object.
        public Database(SqlConnection connection)
        {
            Connection = connection;
        }

        // Om man vill skicka en sträng som argument.
        public Database(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

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
