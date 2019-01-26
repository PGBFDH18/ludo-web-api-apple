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
                return Ok(GamesContainer.ReadFromLoadSaveService().Select(n => $"{n.ID}: {n.Name}"));
            }
            catch (ArgumentNullException)
            {
                return NotFound("Inga sparade spel.");
            }

        }

        // POST /ludo   -Skåpa ett nytt spel
        [HttpPost]
        public void Post([FromBody] string name) => GamesContainer.WriteToMemory(new Game(name));


        // GET /ludo/{gameID}   -Detaljerad information om spelet, som vart alla pjäser finns
        [Route("{gameID}/")] // <- "override" första attributgrejen!
        [HttpGet]
        public ActionResult<Game> Get(int gameID) => Ok(Game.ListAllPieces(gameID));

        // POST /ludo/{gameID}  -Starta ett sparat spel (även spara spelet)
        [Route("{gameID}/")]
        [HttpPost]
        public void Post(int gameID)
        {

        }


        // DELETE /ludo/{gameID}    -Ta bort ett sparat spel
        //[Route("{gameID}")]
        //[HttpDelete]



        //// POST /ludo/{gameID}/players      -Välja antal spelare 
        //[Route("{gameID}/players")]
        //[HttpPost]


        //// GET /ludo/{gameID}/players/{playerID}        -Lista på valbara färger
        //[Route("{gameID}/players/{playerID}")]
        //[HttpGet]


        //// POST /ludo/{gameID}/players/{playerID}       -Välja färg på pjäsen
        //[Route("{gameID}/players/{playerID}")]
        //[HttpPost]


        //// GET /ludo/{gameID}/players/{playerID}/dice       -En spelare slår med tärningen
        //[Route("{gameID}/players/{playerID}/dice")]
        //[HttpGet]


        //// POST /ludo/{gameID}/players/{playerID}/{piece}       -Välja vilken pjäs man vill flytta
        //[Route("{gameID}/players/{playerID}/{piece}")]
        //[HttpPost]
    }
}
