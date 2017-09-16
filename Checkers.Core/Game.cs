using Checkers.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    [Serializable]
    public class Game
    {
        public Player BlackPlayer { get; set; }
        public Player RedPlayer { get; set; }

        public Player CurrentPlayer { get; set; }
        public Board Board { get; set; }

        public Game()
        {
            this.BlackPlayer = new WhitePlayer();
            this.RedPlayer = new RedPlayer();
            this.Board = new Board();
        }

        public void PositionatePieces()
        {
            this.BlackPlayer.PreparePieces();
            this.RedPlayer.PreparePieces();

            this.Board.RedPieces = this.RedPlayer.Pieces;
            this.Board.BlackPieces = this.BlackPlayer.Pieces;
        }

        public void RafflePlayer()
        {
            Random r = new Random();
            this.CurrentPlayer = (r.Next(2) % 2 == 0 ? this.RedPlayer : this.BlackPlayer); 
        }

        public void SwapPlayers()
        {
            this.CurrentPlayer = (this.CurrentPlayer == this.RedPlayer ? this.BlackPlayer : this.RedPlayer);
        }

    }
}
