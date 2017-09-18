using System;
using System.Collections.Generic;
using System.Drawing;
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
            var predictions = new Prediction();

            //Movimento que vai para baixo/direita
            int y = this.Y;
            int x = this.X;
            for (; x < 8 && y < 8; x++, y++)
            {
                if(board[x, y] == null)
                {
                    predictions.RightPrediction.Add(new Point(x, y));
                }
            }


            //Movimento que vai para baixo/esquerda 
            y = this.Y;
            x = this.X;
            for (; x < 8 && y >= 0; x++, y--)
            {
                if (board[x, y] == null)
                {
                    predictions.LeftPrediction.Add(new Point(x, y));
                }
            }

            //Movimento que vai para cima/direita
            y = this.Y;
            x = this.X;
            for (; x >= 0 && y < 8; x--, y++)
            {
                if (board[x, y] == null)
                {
                    predictions.RightPrediction.Add(new Point(x, y));
                }
            }


            //Movimento que vai para cima/esquerda 
            y = this.Y;
            x = this.X;
            for (; x >= 0 && y >= 0; x--, y--)
            {
                if (board[x, y] == null)
                {
                    predictions.LeftPrediction.Add(new Point(x, y));
                }
            }



            return predictions;
        }

        public override KingPiece ToKing()
        {
            return this;
        }

        public override bool TransformToKing()
        {
            return false;
        }
    }
}
