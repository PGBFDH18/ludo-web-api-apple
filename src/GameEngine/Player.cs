using System;
using System.Collections.Generic;

namespace GameEngine
{
    /// <summary>
    /// Represents the participant in the game.
    /// </summary>
    public class Player
    {
        // Spelarens pjäser ligger här!
        public List<Piece> Pieces { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// Initialises a new Player.
        /// </summary>
        /// <param name="number">The player's identity.</param>
        public Player(int number)
        {
            Pieces = new List<Piece>(4);
            this.Number = number;
        }

        public override string ToString() => $"Spelare {Number}";
    }
}
