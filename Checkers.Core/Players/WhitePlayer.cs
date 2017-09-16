using Checkers.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Players
{
    [Serializable]
    public class BlackPlayer : Player
    {
        public override void PreparePieces()
        {
            for (int row = 8 - 3; row < 8; row++)
            {
                for (int column = row % 2; column < 8; column += 2)
                {
                    this.Pieces.Add(new WhitePiece(row, column));
                }
            }
        }
    }
}
