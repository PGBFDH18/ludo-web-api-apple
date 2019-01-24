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
        // GET ludo/
        [HttpGet]
        public ActionResult<List<Game>> Get() => Ok(Game.LoadSaveService.Load());

        // POST ludo/
        [HttpPost]
        public void Post(Game game) => Game.LoadSaveService.Save(game);


        // GET ludo/{gameID}
        [Route("{gameID}/")] // "override" första attributgrejen!
        [HttpGet]
        public ActionResult<Game> Get(int gameID) => Ok(Game.LoadSaveService.Load(gameID));
    }
}
