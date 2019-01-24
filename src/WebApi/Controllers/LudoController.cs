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
        public List<Game> Get()
        {
            //Database database = new Database("Data Source=den1.mssql8.gear.host;User ID=appleludo;Password=**********;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            ILoadSave localDrive = new Drive(@"C:\Users\Johan Wassberg\Desktop", @"C:\Users\Johan Wassberg\Desktop");
            
            // EXEMPEL
            Game game = new Game(new List<Player>
            {
                new Player(1)
                {
                    Pieces = new List<Piece>
                    {
                        new Piece(Piece.PieceColor.Blue, 3, 0, 0),
                        new Piece(Piece.PieceColor.Blue, 1, 0, 0),
                        new Piece(Piece.PieceColor.Blue, 2, 0, 0),
                        new Piece(Piece.PieceColor.Blue, 4, 0, 0),
                    }
                },
                new Player(2)
                {
                    Pieces = new List<Piece>
                    {
                        new Piece(Piece.PieceColor.Yellow, 3, 0, 0),
                        new Piece(Piece.PieceColor.Yellow, 1, 0, 0),
                        new Piece(Piece.PieceColor.Yellow, 2, 0, 0),
                        new Piece(Piece.PieceColor.Yellow, 4, 0, 0),
                    }
                }
            });
            game.Name = "Game 1";
            localDrive.Save(game);
            // EXEMPEL END

            return null;
        }

        // POST ludo/
        [HttpPost]
        public void Post(int numberOfPlayers)
        {
            var game = new Game();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                //game.Players.Add(new Player());
            }
        }

    }
}
