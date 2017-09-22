using Checkers.Core;
using Checkers.Core.Pieces;
using Checkers.Core.Players;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class GameController
    {
        private static GameController instance;

        private Game game;
        private Player player;

        public Player Player
        {
            get
            {
                return this.player;
            }
        }

        public Game Game
        {
            get
            {
                return this.game;
            }
            private set
            {
                this.game = value;
            }
        }


        public bool IsMyTurn
        {
            get
            {
                if (this.Game != null && this.player != null)
                    return (this.Game.CurrentPlayer is WhitePlayer && this.player is WhitePlayer ||
                            this.Game.CurrentPlayer is RedPlayer && this.player is RedPlayer);
                else return false;
            }
        }

        private GameController()
        {
            this.Game = null;
        }

        public static GameController GetInstance()
        {
            if (instance == null)
                instance = new GameController();
            return instance;
        }

        public void Initialize(Game game)
        {

            if(this.Game == null)
                this.Game = game;
        }

        public void Move(Piece piece, Point destination)
        {
            this.Game.Move(piece, destination);
        }

        public void SetWhitePlayer()
        {
            if (this.player == null)
            {
                this.player = this.Game.WhitePlayer;
            }
        }

        public void SetRedPlayer()
        {
            if(this.player == null)
            {
                this.player = this.Game.RedPlayer;
            }
        }
    }
}
