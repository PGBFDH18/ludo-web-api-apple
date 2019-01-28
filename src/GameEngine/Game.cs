using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GameEngine
{
    public class Game
    {
        // Save och Load funktionalitet.
        public static ILoadSave LoadSaveService { get; set; } // Spara till fil eller databas.
        public static IGamesContainer GamesContainer { get; set; } // Spara till minnet.

        // Deltagarna ligger här!
        public List<Player> Players { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }
        public bool GameOver { get; set; }
        public GameState GameState { get; set; }
        private List<PieceColor> colors = new List<PieceColor>
        {
            PieceColor.Red,
            PieceColor.Blue,
            PieceColor.Yellow,
            PieceColor.Green
        };


        public Game()
        {            
        }

        public Game(string name)
        {
            Players = new List<Player>();
            Name = name;
            GameState = GameState.NotStarted;
        }

        public void PlayerTurn(Player player)
        {
            int steps = Dice.Roll();
            //ListAllPieces();

            // Indexerad lista med endast nuvarande spelares pjäser.
            //ListPlayerPieces(player);

            // Spelaren väljer här vilken pjäs som ska flyttas från utifrån listan.
            Console.Write("Mata in index: ");
            int choice = int.Parse(Console.ReadLine()); // Behöver felhantering.
            Piece piece = player.Pieces[choice - 1];    // Behöver felhantering.
            Console.Clear();

            // Kontrollerar om pjäsen är inne i boet och tärningslagen är 1 eller 6.
            if (piece.IsHome && steps == 1 || steps == 6)
            {
                piece.Move(steps);
                //Console.WriteLine($"{piece} flyttade {steps} steg.");
            }
            // Kollar att pjäsen är ute ur boet.
            else if (piece.GetPosition() > 0)
            {
                piece.Move(steps);
                //Console.WriteLine($"{piece} flyttade {steps} steg.");
            }
            else
            {
                //Console.WriteLine($"Du måste slå antingen 1 eller 6 för att flytta ut {piece} ur boet!");
            }

            foreach (var opponent in Players)
            {
                foreach (var opponentPiece in opponent.Pieces)
                {
                    // För att göra det tydligare.
                    Piece myPiece = piece;
                    if (myPiece.IsOnOpponentPosition(opponentPiece) && myPiece.GetPosition() != 0 && opponentPiece.GetPosition() <= 40)
                    {
                        opponentPiece.Home();
                        Console.WriteLine(myPiece + " knuffade ut " + opponentPiece + "!");
                    }
                }
            }

            if (piece.InGoal)
            {
                player.Pieces.RemoveAt(choice - 1);
                if (player.Pieces.Count <= 0)
                {
                    GameOver = true;
                    Console.WriteLine(player + " har vunnit!");
                }
                Console.WriteLine("Pjäsen har nått mål!");
            }

            Console.ReadKey();
        }

        public static List<Piece> ListAllPieces(int gameID)
        {
            // Hämta spelare.
            List<Player> players = LoadSaveService.Load(gameID).Players;

            // Lista som innehåller alla pjäser i spelet.
            var allPlayersPieces = new List<Piece>();

            // Lägg till alla pjäser i listan.
            foreach (var player in players)
                foreach (var piece in player.Pieces)
                    allPlayersPieces.Add(piece);

            return allPlayersPieces;
        }

        /// <summary>
        /// Returns a list of pieces on a specific game and player.
        /// </summary>
        /// <param name="gameID">Game ID of a game on file.</param>
        /// <param name="playerNumber">Number of the player in the game.</param>
        /// <returns></returns>
        public List<Piece> ListPlayerPieces(int gameID, int playerNumber) =>
            LoadSaveService.Load(gameID) // Ladda spel.
            .Players.Where(p => p.Number == playerNumber) // Hämta spelaren från spelet.
            .First().Pieces; // Hämta pjäserna från spelaren.

        /// <summary>
        /// Adds a player to a game which is present in memory.
        /// </summary>
        /// <param name="player">Instance of Player.</param>
        /// <param name="color">Identifies the player and sets piece color.</param>
        public void AddPlayer(PieceColor color)
        {
            Player player = new Player(Players.Count + 1);
            // correctionFactor förskjuts med 10 för varje spelare.
            if (!colors.Contains(color))
                throw new NotSupportedException($"Color '{color.ToString()}' already selected.");

            int correctionFactor = Players.Count * 10;
            for (int i = 0; i < 4; i++)
                player.Pieces.Add(new Piece(color, i + 1, 0, correctionFactor));
            colors.Remove(color);

            // Tas bort.
            if (Players.Count > 3)
                throw new NotSupportedException($"Between 2 and 4 participants required. Participant count: {Players.Count}");
            else
                Players.Add(player);
        }

        public void SetupDone()
        {

        }

        //#region "Övergivna metoder"
        //public void GameSetup()
        //{
        //    // Lista med valbara färger.
        //    var pieceColors = new List<PieceColor>
        //    {
        //        PieceColor.Red,
        //        PieceColor.Blue,
        //        PieceColor.Yellow,
        //        PieceColor.Green
        //    };

        //    Console.WriteLine("Välkommen till PäronLudo!");

        //    bool checker = true;
        //    while (checker)
        //    {
        //        Console.Write("Hur många spelare skall spela? (2-4): ");
        //        numberOfPlayers = int.Parse(Console.ReadLine());

        //        if (numberOfPlayers > 1 && numberOfPlayers < 5)
        //        {
        //            checker = false;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Du måste ange 2-4 spelare!");
        //        }
        //    }

        //    List<Player> players = new List<Player>(4);

        //    for (int i = 0, correctionFactor = 0; i < numberOfPlayers; i++, correctionFactor += 10)
        //    {
        //        Player player = new Player(i + 1);
        //        //// Skriv ut lista på valbara färger.
        //        for (int j = 0; j < pieceColors.Count; j++)
        //        {
        //            Console.WriteLine($"[{j + 1}]: " + pieceColors[j]);
        //        }
        //        Console.WriteLine();

        //        // Val av färg.
        //        Console.Write($"Spelare {i + 1} välj färg att spela med: ");
        //        var index = int.Parse(Console.ReadLine());
        //        Console.Clear();

        //        // Tilldela startpostitioner till pjäser.                
        //        // Lägger till 4 pjäser per färg.
        //        for (int k = 0; k < 4; k++)
        //        {
        //            PieceColor color = pieceColors[index - 1];
        //            Piece piece = new Piece(color, k + 1, 0, correctionFactor);
        //            player.Pieces.Add(piece);
        //        }
        //        pieceColors.RemoveAt(index - 1);
        //        players.Add(player);
        //    }

        //    //Game = new Game(players, databaseConnectionString);
        //}
        //#endregion

        //public static void AddToMemory(Game game) => games.Add(game);

        //public static Game LoadFromMemory(int gameID) => games.Where(g => g.ID == gameID).First();      
    }
}
