using System;

namespace GameEngine
{
    /// <summary>
    /// Represents the player's piece.
    /// </summary>
    public class Piece
    {
        public PieceColor Color { get; set; }
        public int Number { get; set; }
        public int CorrectionFactor { get; set; }
        public int Position { get; set; }
        public bool IsHome => Position == 0;
        public bool InGoal => Position >= 45;

        /// <summary>
        /// Initialises a new instance of Piece.
        /// </summary>
        /// <param name="color">First identity.</param>
        /// <param name="number">Second identity.</param>
        /// <param name="position">Assigns local default position to piece.</param>
        /// <param name="correctionFactor">Integer which is taken into account when calculating the piece's global position.</param>
        public Piece(PieceColor color, int number, int position, int correctionFactor)
        {
            Color = color;
            Number = number;
            Position = position;
            CorrectionFactor = correctionFactor;
        }

        // Flytta pjäsen 'steps' antal steg.
        public void Move(int steps) => Position += steps;

        // Hämta pjäsens "lokala position".
        public int GetPosition() => Position;

        // Hämta pjäsens "globala position".
        public int GetAbsolutePosition()
        {
            if (Position + CorrectionFactor > 40)
                return Position + CorrectionFactor - 40;
            else if (Position is 0)
                return 0;
            else
                return Position + CorrectionFactor;
        }

        public void Home() => Position = 0;

        // Metod som avgör om spelarens pjäs har samma position som motståndarens position med hänsyn till korrigeringsfaktorn.
        public bool IsOnOpponentPosition(Piece opponentPiece) =>
            GetAbsolutePosition() == opponentPiece.GetAbsolutePosition() && this != opponentPiece;

        // Exempelresultat: 'R3' om pjäsen är Röd och har siffra 3.
        public override string ToString() => $"{Color.ToString()[0]}{Number}";
    }
}
