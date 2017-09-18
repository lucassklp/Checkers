using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    [Serializable]
    public class WhitePiece : Piece
    {
        public WhitePiece(int x, int y) : base(x, y)
        {

        }

        public override bool IsEnemyPiece(Piece p)
        {
            return (!(p is WhitePiece));
        }

        public override Prediction Predict(Board Board)
        {
            var predictions = new Prediction();

            //Valida as peças vizinhas: (Peças de frente)
            Piece slot1 = null;
            if (this.Y > 0)
            {
                slot1 = Board[this.X - 1, this.Y - 1];
                if(slot1 == null)
                {
                    predictions.LeftPrediction.Add(new Point(this.X - 1, this.Y - 1));
                }
            }
            
            Piece slot2 = null;
            if(this.Y < 7)
            {
                slot2 = Board[this.X - 1, this.Y + 1];
                if(slot2 == null)
                {
                    predictions.RightPrediction.Add(new Point(this.X - 1, this.Y + 1));
                }
            }


            //Comer pra frente
            if(this.X > 1)
            {
                if(this.Y < 6) //Posição válida para comer no Y pela direita
                {
                    var slot3 = Board[this.X - 1, this.Y + 1];
                    if (slot3 != null)
                    {
                        if (this.IsEnemyPiece(slot3))
                        {
                            if (Board[this.X - 2, this.Y + 2] == null)
                            {
                                predictions.RightPrediction.Add(new Point(this.X - 2, this.Y + 2));
                            }
                        }
                    }
                }

                if(this.Y > 1) //Posição válida para comer no Y pela esquerda
                {
                    var slot4 = Board[this.X - 1, this.Y - 1];
                    if (slot4 != null)
                    {
                        if (this.IsEnemyPiece(slot4))
                        {
                            if (Board[this.X - 2, this.Y - 2] == null)
                            {
                                predictions.LeftPrediction.Add(new Point(this.X - 2, this.Y - 2));
                            }
                        }
                    }
                }
            }


            //Comer pra trás
            if(this.X < 6) //Posicão válida pra comer no X
            {
                if (this.Y < 6) //Posição válida para comer no Y pela direita
                {
                    var slot3 = Board[this.X + 1, this.Y + 1];
                    if (slot3 != null)
                    {
                        if (this.IsEnemyPiece(slot3))
                        {
                            if (Board[this.X + 2, this.Y + 2] == null)
                            {
                                predictions.RightPrediction.Add(new Point(this.X + 2, this.Y + 2));
                            }
                        }
                    }
                }
                
                if(this.Y > 1) //Posição válida para comer no Y pela esquerda
                { 
                    var slot4 = Board[this.X + 1, this.Y - 1];
                    if (slot4 != null)
                    {
                        if (this.IsEnemyPiece(slot4))
                        {
                            if (Board[this.X + 2, this.Y - 2] == null)
                            {
                                predictions.LeftPrediction.Add(new Point(this.X + 2, this.Y - 2));
                            }
                        }
                    }
                }
            }

            return predictions;
        }        
        
        public override KingPiece ToKing()
        {
            return new KingPiece(this);
        }

        public override bool TransformToKing()
        {
            return (this.X == 0);
        }
    }
}
