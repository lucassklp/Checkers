﻿using System;
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
            if (p == null)
                return false;
            return this.piece.IsEnemyPiece(p);
        }

        public override Prediction Predict(Board board)
        {
            var predictions = new Prediction();

            //Movimento que vai para baixo/direita
            int y = this.Y + 1;
            int x = this.X + 1;
            for (; x < 8 && y < 8; x++, y++)
            {
                if(board[x, y] == null)
                {
                    predictions.Predictions.Add(new Point(x, y));
                }
                else
                {
                    //Following
                    if(board[x + 1, y + 1] != null)
                    {
                        break;
                    }
                }
            }


            //Movimento que vai para baixo/esquerda 
            y = this.Y - 1;
            x = this.X + 1;
            for (; x < 8 && y >= 0; x++, y--)
            {
                if (board[x, y] == null)
                {
                    predictions.Predictions.Add(new Point(x, y));
                }
                else
                {
                    //Following
                    if (board[x + 1, y - 1] != null)
                    {
                        break;
                    }
                }
            }

            //Movimento que vai para cima/direita
            y = this.Y + 1;
            x = this.X - 1;
            for (; x >= 0 && y < 8; x--, y++)
            {
                if (board[x, y] == null)
                {
                    predictions.Predictions.Add(new Point(x, y));
                }
                else
                {
                    //Following
                    if (board[x - 1, y + 1] != null)
                    {
                        break;
                    }
                }
            }


            //Movimento que vai para cima/esquerda 
            y = this.Y - 1;
            x = this.X - 1;
            for (; x >= 0 && y >= 0; x--, y--)
            {
                if (board[x, y] == null)
                {
                    predictions.Predictions.Add(new Point(x, y));
                }
                else
                {
                    //Following
                    if (board[x - 1, y - 1] != null)
                    {
                        break;
                    }
                }
            }



            return predictions;
        }

        public override Prediction PredictToEat(Board board)
        {
            var predictionsToEat = new Prediction();

            //Movimento que come pra baixo/esquerda 

            if (this.Y > 1 && this.X < 6)
            {
                if (board[this.X + 1, this.Y - 1] != null)
                {
                    if (this.IsEnemyPiece(board[this.X + 1, this.Y - 1]))
                    {
                        if (board[this.X + 2, this.Y - 2] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 2, this.Y - 2));
                        }
                    }
                }
                else if (this.IsEnemyPiece(board[this.X + 2, this.Y - 2]))
                {
                    if (board[this.X + 3, this.Y - 3] == null)
                    {
                        predictionsToEat.Predictions.Add(new Point(this.X + 3, this.Y - 3));
                    }
                }
                else if (board[this.X + 2, this.Y - 2] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 3, this.Y - 3]))
                    {
                        if (board[this.X + 4, this.Y - 4] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 4, this.Y - 4));
                        }
                    }
                }
                else if (board[this.X + 3, this.Y - 3] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 4, this.Y - 4]))
                    {
                        if (board[this.X + 5, this.Y - 5] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 5, this.Y - 5));
                        }
                    }
                }
                else if (board[this.X + 4, this.Y - 4] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 5, this.Y - 5]))
                    {
                        if (board[this.X + 6, this.Y - 6] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 6, this.Y - 6));
                        }
                    }
                }
            }
            //Movimento que come pra baixo/direita
            if (this.Y < 6 && this.X < 6)
            {
                if (board[this.X + 1, this.Y + 1] != null)
                {
                    if (this.IsEnemyPiece(board[this.X + 1, this.Y + 1]))
                    {
                        if (board[this.X + 2, this.Y + 2] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 2, this.Y + 2));
                        }
                    }
                }             
                else if (this.IsEnemyPiece(board[this.X + 2, this.Y + 2]))
                {
                    if (board[this.X + 3, this.Y + 3] == null)
                    {
                        predictionsToEat.Predictions.Add(new Point(this.X + 3, this.Y + 3));
                    }
                }
                else if (board[this.X + 2, this.Y + 2] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 3, this.Y + 3]))
                    {
                        if (board[this.X + 4, this.Y + 4] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 4, this.Y + 4));
                        }
                    }
                }
                else if (board[this.X + 3, this.Y + 3] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 4, this.Y + 4]))
                    {
                        if (board[this.X + 5, this.Y + 5] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 5, this.Y + 5));
                        }
                    }
                }
                else if (board[this.X + 4, this.Y + 4] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 5, this.Y + 5]))
                    {
                        if (board[this.X + 6, this.Y + 6] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 6, this.Y + 6));
                        }
                    }
                }
                else if (board[this.X + 5, this.Y + 5] == null)
                {
                    if (this.IsEnemyPiece(board[this.X + 6, this.Y + 6]))
                    {
                        if (board[this.X + 7, this.Y + 7] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X + 7, this.Y + 7));
                        }
                    }
                }


            }
            //Movimento que come pra cima/direita
            if (this.X > 1 && this.Y < 6)
            {
                if (board[this.X - 1, this.Y + 1] != null)
                {
                    if (this.IsEnemyPiece(board[this.X - 1, this.Y + 1]))
                    {
                        if (board[this.X - 2, this.Y + 2] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 2, this.Y + 2));
                        }
                    }
                }
                else if(this.IsEnemyPiece(board[this.X - 2, this.Y + 2]))
                {
                    if(board[this.X-3,this.Y+3] == null)
                    {
                        predictionsToEat.Predictions.Add(new Point(this.X - 3, this.Y + 3));
                    }
                }
                else if (board[this.X - 2, this.Y + 2] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 3, this.Y + 3]))
                    {
                        if (board[this.X - 4, this.Y + 4] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 4, this.Y + 4));
                        }
                    }
                }
                else if (board[this.X - 3, this.Y + 3] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 4, this.Y + 4]))
                    {
                        if (board[this.X - 5, this.Y + 5] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 5, this.Y + 5));
                        }
                    }
                }
                else if (board[this.X - 4, this.Y + 4] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 5, this.Y + 5]))
                    {
                        if (board[this.X - 6, this.Y + 6] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 6, this.Y + 6));
                        }
                    }
                }

            }
            //Movimento que come pra cima/esquerda
            if (this.Y > 1 && this.X > 1)
            {
                if (board[this.X - 1, this.Y - 1] != null)
                {
                    if (this.IsEnemyPiece(board[this.X - 1, this.Y - 1]))
                    {
                        if (board[this.X - 2, this.Y - 2] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 2, this.Y - 2));
                        }
                    }
                }
                else if(this.IsEnemyPiece(board[this.X-2,this.Y-2]))
                {
                    if(board[this.X - 3, this.Y - 3] == null)
                    {
                        predictionsToEat.Predictions.Add(new Point(this.X - 3, this.Y - 3));
                    }
                }
                else if (board[this.X - 2, this.Y - 2] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 3, this.Y - 3]))
                    {
                        if (board[this.X - 3, this.Y - 3] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 3, this.Y - 3));
                        }
                    }
                }
                else if (board[this.X - 3, this.Y - 3] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 4, this.Y - 4]))
                    {
                        if (board[this.X - 4, this.Y - 4] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 4, this.Y - 4));
                        }
                    }
                }
                else if (board[this.X - 4, this.Y - 4] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 5, this.Y - 5]))
                    {
                        if (board[this.X - 6, this.Y - 6] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 6, this.Y - 6));
                        }
                    }
                }
                else if (board[this.X - 5, this.Y - 5] == null)
                {
                    if (this.IsEnemyPiece(board[this.X - 6, this.X - 6]))
                    {
                        if (board[this.X - 7, this.Y - 7] == null)
                        {
                            predictionsToEat.Predictions.Add(new Point(this.X - 7, this.Y - 7));
                        }
                    }
                }
            }
            return predictionsToEat;
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
