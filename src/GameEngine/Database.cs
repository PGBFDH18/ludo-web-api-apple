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
            var connection = Connection;
            //SqlParameter parameter = new SqlParameter("@id", )
            List<dynamic> games = connection.Query("SELECT * FROM Game WHERE ID = @id").AsList();
            throw new NotImplementedException();
        }

        public List<Game> Load()
        {
            //CommandDefinition commandDefinition = new CommandDefinition();
            //commandDefinition.CommandText = 
            string sql = "SELECT * FROM Game";
      
            using (var connection = Connection)
            {
                throw new NotImplementedException();
                //Guid guid = new Guid();
                
                //return connection.Query(sql).AsList(); // TODO: Cast till Game på något sätt.

                //"SELECT Name = @Name FROM Game", new Game(new List<Player>()) { Name = "@Name" })
            }

            throw new NotImplementedException();
        }
    }
}
