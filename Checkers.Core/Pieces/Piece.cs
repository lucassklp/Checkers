using Checkers.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    public abstract class Piece
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public bool IsKing { get; private set; } = false;

        public Piece(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public abstract Prediction Predict(Board board);

        public abstract void Move(Moviment moviment);

        public abstract bool IsMovimentValid(Moviment moviment);
    }
}
