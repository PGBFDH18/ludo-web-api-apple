using System;

namespace GameEngine
{
    /// <summary>
    /// Represents the player's piece.
    /// </summary>
    public class Piece
    {
        public enum PieceColor
        {
            Red, Blue, Yellow, Green
        }

        public PieceColor Color { get; set; }
        public int Number { get; set; }
        public int correctionFactor { get; set; }
        private int position;

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
            this.position = position;
            this.correctionFactor = correctionFactor;
        }

        // Flytta pjäsen 'steps' antal steg.
        public void Move(int steps) => position += steps;

        // Hämta pjäsens "lokala position".
        public int GetPosition() => position;

        // Hämta pjäsens "globala position".
        public int GetAbsolutePosition()
        {
            if (position + correctionFactor > 40)
                return position + correctionFactor - 40;
            else if (position is 0)
                return 0;
            else
                return position + correctionFactor;
        }

        public void Home() => position = 0;

        public bool IsHome() => position == 0;

        public bool InGoal() => position >= 45;

        // Ersatts av instansvariabeln. Överge?
        public static bool IsOnOpponentPosition(Piece myPiece, Piece opponentPiece) =>
            myPiece.GetAbsolutePosition() == opponentPiece.GetAbsolutePosition() && opponentPiece != myPiece;

        // Metod som avgör om spelarens pjäs har samma position som motståndarens position med hänsyn till korrigeringsfaktorn.
        public bool IsOnOpponentPosition(Piece opponentPiece) =>
            GetAbsolutePosition() == opponentPiece.GetAbsolutePosition() && this != opponentPiece;

        // Exempelresultat: 'R3' om pjäsen är Röd och har siffra 3.
        public override string ToString() => $"{Color.ToString()[0]}{Number}";
    }
}
