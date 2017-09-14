using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    public class KingPiece : Piece
    {
        public KingPiece(Piece p) : base(p.X, p.Y)
        {

        }

        public override bool IsMovimentValid(Moviment moviment)
        {
            throw new NotImplementedException();
        }

        public override void Move(Moviment moviment)
        {
            throw new NotImplementedException();
        }

        public override Prediction Predict(Board board)
        {
            throw new NotImplementedException();
        }

        public override KingPiece ToKing()
        {
            return this;
        }
    }
}
