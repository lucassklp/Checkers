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
        private Piece piece;

        public KingPiece(Piece p) : base(p.X, p.Y)
        {
            this.piece = p;
        }

        public override bool IsEnemyPiece(Piece p)
        {
            return p.IsEnemyPiece(p);
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
