using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Dapper;

namespace GameEngine
{
    // Observera! Övriga felhanteringar ska ske utanför klassen.

    /// <summary>
    /// Store game state on database.
    /// </summary>
    public class Database : ILoadSave
    {
        public string Source { get; set; } // <- Connection string.

        // Om man vill skicka SqlConnection object.
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

            var connection = new SqlConnection(Source);
            List<dynamic> games = connection.Query("SELECT * FROM Game WHERE ID = @id").AsList();
        }

        public List<Game> Load()
        {
            throw new NotImplementedException();

            string sql = "SELECT * FROM Game";
      
            using (var connection = new SqlConnection(Source))
            {
                throw new NotImplementedException();
                //Guid guid = new Guid();
                
                //return connection.Query(sql).AsList(); // TODO: Cast till Game på något sätt.

                //"SELECT Name = @Name FROM Game", new Game(new List<Player>()) { Name = "@Name" })
            }
        }
    }
}
