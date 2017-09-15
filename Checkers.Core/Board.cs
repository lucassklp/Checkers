using Checkers.Core.Pieces;
using Checkers.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    [Serializable]
    public class Board
    {
        private const int BOARD_SIZE = 8;

        public List<Piece> BlackPieces { get; set; }
        public List<Piece> RedPieces { get; set; }


        public Board()
        {
            this.BlackPieces = new List<Piece>();
            this.RedPieces = new List<Piece>();
        }

        public Piece this[int x, int y]
        {
            get
            {
                var pesq1 = this.BlackPieces.Find(piece => piece.X == x && piece.Y == y);
                if(pesq1 == null)
                {
                    return this.RedPieces.Find(piece => piece.X == x && piece.Y == y);
                }
                return pesq1;
            }
            
        }

    }
}
