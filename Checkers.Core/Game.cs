using Checkers.Core.Pieces;
using Checkers.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    [Serializable]
    public class Game
    {
        public Player WhitePlayer { get; set; }
        public Player RedPlayer { get; set; }

        public Player CurrentPlayer { get; set; }
        public Board Board { get; set; }

        public Game()
        {
            this.WhitePlayer = new WhitePlayer();
            this.RedPlayer = new RedPlayer();
            this.Board = new Board();
        }

        public void PositionatePieces()
        {
            this.WhitePlayer.PreparePieces();
            this.RedPlayer.PreparePieces();

            this.Board.RedPieces = this.RedPlayer.Pieces;
            this.Board.WhitePieces = this.WhitePlayer.Pieces;
        }

        public void Move(Piece piece, Point destination)
        {
            var p = this.Board[piece.X, piece.Y];
            p.Move(this.Board, destination);
        }

        public void RafflePlayer()
        {
            Random r = new Random();
            this.CurrentPlayer = this.WhitePlayer;//(r.Next(2) % 2 == 0 ? this.RedPlayer : this.WhitePlayer); 
        }

        public void SwapPlayers()
        {
            this.CurrentPlayer = (this.CurrentPlayer == this.RedPlayer ? this.WhitePlayer : this.RedPlayer);
        }

    }
}
