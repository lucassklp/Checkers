using Checkers.Core;
using Checkers.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    interface IRemoteGame
    {
        void Initialize(Game game);
        void Move(Piece piece, Point destination);
        void SwapPlayer();
        void SetRedPlayer();
    }
}
