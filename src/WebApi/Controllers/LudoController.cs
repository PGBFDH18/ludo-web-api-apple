using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameEngine;

namespace WebApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class LudoController : ControllerBase
    {
        // GET ludo/    -Lista sparade fia spel
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            try
            {
                return Ok(Game.LoadSaveService.Load().Select(n => $"{n.ID}: {n.Name}"));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Inga sparade spel.");
            }

        }


        // POST /ludo/{gameID}  -Starta ett sparat spel (även spara spelet)
        [Route("{gameID}/")]
        [HttpPost]
        public void StartGame(int gameID)
        {
            Game game = Game.GamesContainer.Load(gameID);
            Game.LoadSaveService.Save(game);
        }

        // GET /ludo/{gameID}/save
        [Route("{gameID}/save")]
        [HttpGet]
        public ActionResult<string> Save(int gameID)
        {
            Game game = Game.GamesContainer.Load(gameID); // Ladda in spelet.
            Game.LoadSaveService.Save(game); // Spara till fil eller databas.
            Game.GamesContainer.Delete(gameID); // Radera från minnet.

            return Ok("Sparat.");
        }

        // POST /ludo   -Skåpa ett nytt spel
        [HttpPost]
        public void Post([FromBody] string name) => Game.GamesContainer.Add(new Game(name));
                       


        //// GET /ludo/{gameID}/players/{playerID}/dice       -Visa tärningsslaget. Lägg värde i player.
        [Route("{gameID}/players/{playerID}/dice")]
        [HttpGet]
        public ActionResult<int> RollDice(int gameID, int playerID)
        {
            Game game = Game.LoadSaveService.Load(gameID); // Hämta spelet från fil eller databas.
            Player player = game.Players.Where(p => p.Number == playerID).First(); // Hämta spelaren i spelet.
            player.DiceValue = Dice.Roll(); // Spara tärningsvärdet i spelaren.
            Game.LoadSaveService.Update(game); // Uppdatera spelet i filen.

            return Ok(player.DiceValue);
        }

        // GET /ludo/{gameID}   -Detaljerad information om spelet, som vart alla pjäser finns
        [Route("{gameID}/")] // <- "override" första attributgrejen!
        [HttpGet]
        // Bug: Kraschar om antal sparade spel är mindre än 2.
        public ActionResult<Game> Get(int gameID) => Ok(Game.ListAllPieces(gameID));

        //// POST /ludo/{gameID}/players/{playerID}/{pieceID}       -Välja vilken pjäs man vill flytta
        [Route("{gameID}/players/{playerID}/{pieceID}")]
        [HttpPost]
        public ActionResult<string> MovePiece(int gameID, int playerID, int pieceID)
        {
            Game game = Game.LoadSaveService.Load(gameID); // Läs in spelet.
            Player player = game.Players.Where(p => p.Number == playerID).First(); // Läs in spelaren från spelet.
            Piece piece = player.Pieces.Where(p => p.Number == pieceID).First(); // Läs in pjäsen från spelare.
            piece.Move(player.DiceValue); // Flytta pjäsen.
            
            Game.LoadSaveService.Update(game); // Uppdatera spelet i filen.

            return Ok($"Moved piece {player.DiceValue} steps.");
        }


        // DELETE /ludo/{gameID}    -Ta bort ett sparat spel
        [Route("{gameID}")]
        [HttpDelete]
        public void DeleteGame(int gameID)
        {
            Game.LoadSaveService.Delete(gameID);
        }



        //// POST /ludo/{gameID}/players      -Skapa ny spelare med färg. Min anrop: 2. Max anrop: 4.
        [Route("{gameID}/players")]
        [HttpPost]
        public ActionResult<string> AddPlayersToGame(int gameID, [FromBody] PieceColor color)
        {
            try
            {
                Game.GamesContainer.Load(gameID).AddPlayer(color);
                return Ok("Successfully added player.");
            }
            catch (NotSupportedException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
