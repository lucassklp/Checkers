using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    class BlackPiece : Piece
    {
        public BlackPiece(int x, int y) : base(x, y)
        {

        }

        public override void Predict(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
