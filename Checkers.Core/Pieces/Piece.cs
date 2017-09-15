using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    [Serializable]
    public abstract class Piece
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Piece(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public abstract Prediction Predict(Game board);

        public abstract void Move(Moviment moviment);

        public abstract bool IsMovimentValid(Moviment moviment);

        public abstract KingPiece ToKing();
    }
}
