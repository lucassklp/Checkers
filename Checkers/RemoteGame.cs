using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Core;
using Checkers.Core.Pieces;
using System.Drawing;

namespace Checkers
{
    class RemoteGame : MarshalByRefObject, IRemoteGame
    {

        public void Initialize(Game game)
        {
            var gameController = GameController.GetInstance();
            gameController.Initialize(game);
        }

        public void Move(Piece piece, Point destination)
        {
            var gameController = GameController.GetInstance();
            gameController.Game.Move(piece, destination);
        }

        public void SetRedPlayer()
        {
            var gameController = GameController.GetInstance();
            gameController.SetRedPlayer();
        }

        public void SwapPlayer()
        {
            var gameController = GameController.GetInstance();
            gameController.Game.SwapPlayers();
        }
    }
}
