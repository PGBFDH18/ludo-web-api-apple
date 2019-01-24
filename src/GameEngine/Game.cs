using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine
{
    public class Game
    {
        // Save och Load funktionalitet.
        public static ILoadSave LoadSaveService { get; set; }

        // Deltagarna ligger här!
        public List<Player> Players { get; set; }

        public string Name { get; set; }

        public int GameID { get; set; }

        public bool GameOver { get; set; }

        private int numberOfPlayers;

        public Game(List<Player> players)
        {
            Players = players;
            //LoadSaveMethod = new Database(databaseConnectionString);
        }

        public Game()
        {

        }

        public void PlayerTurn(Player player)
        {
            int steps = Dice.Roll();
            //ListAllPieces();

            // Indexerad lista med endast nuvarande spelares pjäser.
            ListPlayerPieces(player);

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

        public void ListAllPieces(int gameID)
        {
            //// För varje spelare...
            //Console.WriteLine("Piece, Position, AbsolutePosition");
            //foreach (var player in Players)
            //{
            //    // För varje pjäs i spelare...
            //    foreach (var piece in player.Pieces)
            //    {
            //        Console.WriteLine($"{piece}, {piece.GetPosition()}, {piece.GetAbsolutePosition()}");
            //    }
            //}

            
        }

        public void ListPlayerPieces(Player player)
        {
            // Indexerad lista med endast nuvarande spelares pjäser.
            for (int i = 0; i < player.Pieces.Count; i++)
            {
                Console.WriteLine($"[{i + 1}]: {player.Pieces[i]}, {player.Pieces[i].GetPosition()}, {player.Pieces[i].GetAbsolutePosition()}");
            }
            Console.WriteLine();
        }

        public void GameSetup()
        {
            // Lista med valbara färger.
            var pieceColors = new List<PieceColor>
            {
                PieceColor.Red,
                PieceColor.Blue,
                PieceColor.Yellow,
                PieceColor.Green
            };

            Console.WriteLine("Välkommen till PäronLudo!");

            bool checker = true;
            while (checker)
            {
                Console.Write("Hur många spelare skall spela? (2-4): ");
                numberOfPlayers = int.Parse(Console.ReadLine());

                if (numberOfPlayers > 1 && numberOfPlayers < 5)
                {
                    checker = false;
                }
                else
                {
                    Console.WriteLine("Du måste ange 2-4 spelare!");
                }
            }

            List<Player> players = new List<Player>(4);

            for (int i = 0, correctionFactor = 0; i < numberOfPlayers; i++, correctionFactor += 10)
            {
                Player player = new Player(i + 1);
                //// Skriv ut lista på valbara färger.
                for (int j = 0; j < pieceColors.Count; j++)
                {
                    Console.WriteLine($"[{j + 1}]: " + pieceColors[j]);
                }
                Console.WriteLine();

                // Val av färg.
                Console.Write($"Spelare {i + 1} välj färg att spela med: ");
                var index = int.Parse(Console.ReadLine());
                Console.Clear();

                // Tilldela startpostitioner till pjäser.                
                // Lägger till 4 pjäser per färg.
                for (int k = 0; k < 4; k++)
                {
                    PieceColor color = pieceColors[index - 1];
                    Piece piece = new Piece(color, k + 1, 0, correctionFactor);
                    player.Pieces.Add(piece);
                }
                pieceColors.RemoveAt(index - 1);
                players.Add(player);
            }

            //Game = new Game(players, databaseConnectionString);
        }
    }
}
