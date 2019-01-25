using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace GameEngine
{
    // Observera! Övriga felhanteringar ska ske utanför klassen.

    /// <summary>
    /// Store game state on local drive.
    /// </summary>
    public class Drive : ILoadSave
    {
        public string Source { get; set; }

        public Drive(string path)
        {
            Source = path;
        }

        /// <summary>
        /// Writes game to file as JSON.
        /// </summary>
        /// <param name="game"></param>
        public void Save(Game game)
        {
            List<Game> existingGames = Load();
            try
            {
                // existingGames kan vara null!
                existingGames.Add(game);
                string json = JsonConvert.SerializeObject(existingGames);
                File.WriteAllText(Source, json);
            }
            catch (NullReferenceException)
            {
                // Om existingGames är null, lägg till game som det enda objektet i filen.
                string json = JsonConvert.SerializeObject(game);
                File.WriteAllText(Source, json);
            }

        }

        public Game Load(int gameID)
        {
            string json = File.ReadAllText(Source);
            var games = JsonConvert.DeserializeObject<IEnumerable<Game>>(json);

            return games.Where(g => g.ID == gameID).First();
        }

        public List<Game> Load()
        {
            string json = File.ReadAllText(Source);
            // TODO: Ska en lista med ett enda objekt returneras?
            try
            {
                // Returnera en List med spel.
                var games = JsonConvert.DeserializeObject<List<Game>>(json);
                return games;
            }
            catch (JsonSerializationException)
            {                
                // Obs! List med bara ETT spel.
                var game = new List<Game>(1)
                {
                    // JSON-array genereras från List.
                    JsonConvert.DeserializeObject<Game>(json)
                }; 
                return game;
            }
        }
    }
}
