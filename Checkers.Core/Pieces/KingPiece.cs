using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    [Serializable]
    public class KingPiece : Piece
    {
        public KingPiece(Piece p) : base(p.X, p.Y)
        {

        }

        public override bool IsMovimentValid(Moviment moviment)
        {
            return true;
        }

        public override void Move(Moviment moviment)
        {
            this.X = moviment.Destination.X;
            this.Y = moviment.Destination.Y;
        }

        public override Prediction Predict(Game board)
        {
            throw new NotImplementedException();
        }

        public override KingPiece ToKing()
        {
            return this;
        }
    }
}
