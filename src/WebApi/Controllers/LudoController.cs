using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameEngine;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class LudoController : ControllerBase
    {
        // GET ludo/
        [HttpGet]
        public ActionResult<List<Game>> Get() => new GamesContainer().Load();

        // POST ludo/
        [HttpPost]
        public void Post(Game game) => new GamesContainer().Save(game);


        // GET ludo/{gameID}
        [Route("{gameID}/")] // "override" första attributgrejen!
        [HttpGet]
        public ActionResult<Game> Get(int gameID) => new GamesContainer().Load(gameID);
    }
}
