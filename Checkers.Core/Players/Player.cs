using Checkers.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Players
{
    public abstract class Player
    {
        public Player()
        {
            this.Pieces = new List<Piece>();
        }

        public List<Piece> Pieces { get; set; }

        public abstract void PreparePieces();

    }
}
