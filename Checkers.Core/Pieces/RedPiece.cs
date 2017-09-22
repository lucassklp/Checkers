using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    [Serializable]
    class RedPiece : Piece
    {
        public RedPiece(int x, int y) : base(x, y)
        {

        }

        public override bool IsEnemyPiece(Piece p)
        {
            return (!(p is RedPiece));
        }

        public bool IsMovimentValid()
        {
            return true;
        }

        public override Prediction Predict(Board Board)
        {
            var predictions = new Prediction();


            //Valida as peças vizinhas: (Peças de frente)
            Piece slot1 = null;
            if (this.Y > 0)
            {
                slot1 = Board[this.X + 1, this.Y - 1];
                if (slot1 == null)
                {
                    predictions.Predictions.Add(new Point(this.X + 1, this.Y - 1));
                }
            }

            Piece slot2 = null;
            if (this.Y < 7)
            {
                slot2 = Board[this.X + 1, this.Y + 1];
                if (slot2 == null)
                {
                    predictions.Predictions.Add(new Point(this.X + 1, this.Y + 1));
                }
            }

            return predictions;
        }

        public override Prediction PredictToEat(Board board)
        {
            var predictionsToEat = new Prediction();

            //Comer pra frente
            if (this.X < 6)
            {
                if (this.Y < 6) //Posição válida para comer no Y pela direita
                {
                    var slot3 = board[this.X + 1, this.Y + 1];
                    if (slot3 != null)
                    {
                        if (this.IsEnemyPiece(slot3))
                        {
                            if (board[this.X + 2, this.Y + 2] == null)
                            {
                                predictionsToEat.Predictions.Add(new Point(this.X + 2, this.Y + 2));
                            }
                        }
                    }
                }

                if (this.Y > 1) //Posição válida para comer no Y pela esquerda
                {
                    var slot4 = board[this.X + 1, this.Y - 1];
                    if (slot4 != null)
                    {
                        if (this.IsEnemyPiece(slot4))
                        {
                            if (board[this.X + 2, this.Y - 2] == null)
                            {
                                predictionsToEat.Predictions.Add(new Point(this.X + 2, this.Y - 2));
                            }
                        }
                    }
                }
            }


            //Comer pra trás
            if (this.X < 6) //Posicão válida pra comer no X
            {
                if (this.Y < 6) //Posição válida para comer no Y pela direita
                {
                    var slot3 = board[this.X - 1, this.Y + 1];
                    if (slot3 != null)
                    {
                        if (this.IsEnemyPiece(slot3))
                        {
                            if (board[this.X - 2, this.Y + 2] == null)
                            {
                                predictionsToEat.Predictions.Add(new Point(this.X - 2, this.Y + 2));
                            }
                        }
                    }
                }

                if (this.Y > 1) //Posição válida para comer no Y pela esquerda
                {
                    var slot4 = board[this.X - 1, this.Y - 1];
                    if (slot4 != null)
                    {
                        if (this.IsEnemyPiece(slot4))
                        {
                            if (board[this.X - 2, this.Y - 2] == null)
                            {
                                predictionsToEat.Predictions.Add(new Point(this.X - 2, this.Y - 2));
                            }
                        }
                    }
                }
            }

            return predictionsToEat;
        }

        public override KingPiece ToKing()
        {
            return new KingPiece(this);
        }

        public override bool TransformToKing()
        {
            return (this.X == 7);
        }
    }
}
