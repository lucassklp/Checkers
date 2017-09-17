using System;
using System.Collections.Generic;
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

        public override bool IsMovimentValid(Moviment moviment)
        {
            return true;
        }

        public override void Move(Moviment moviment)
        {
            this.X = moviment.Destination.X;
            this.Y = moviment.Destination.Y;
        }

        public override Prediction Predict(Game board)
        {
            /*
            //Verifica se a peça está na primeira coluna
            if (this.Y == 0)
            {
                if (this.X == 0)
                {
                    if (board.Board[1, 1] == null)
                    {
                        //Posição livre;

                    }


                }
                else if (this.X == 2)
                {
                    if (board.Board[3, 1] == null)
                    {
                        //Posição livre;
                    }
                }
                else if (this.X == 4)
                {

                    if (board.Board[5, 1] == null)
                    {
                        //Posição livre;
                    }
                }
                else if (this.X == 6)
                {
                    if (board.Board[7, 1] == null)
                    {
                        //Posição livre;
                    }
                }
            }// Verifica se a peça está na segunda coluna
            else if (this.Y == 1)
            {
                if (this.X == 1)
                {

                }
                else if (this.X == 3)
                {

                }
                else if (this.X == 5)
                {

                }
                else if (this.X == 7)
                {

                }
            }
            else if (this.Y == 2)
            {
                if (this.X == 0)
                {


                }
                else if (this.X == 2)
                {

                }
                else if (this.X == 4)
                {


                }
                else if (this.X == 6)
                {

                }
            }
            else if (this.Y == 3)
            {
                if (this.X == 1)
                {

                }
                else if (this.X == 3)
                {

                }
                else if (this.X == 5)
                {

                }
                else if (this.X == 7)
                {

                }
            }
            else if (this.Y == 4)
            {
                if (this.X == 0)
                {


                }
                else if (this.X == 2)
                {

                }
                else if (this.X == 4)
                {


                }
                else if (this.X == 6)
                {

                }
            }
            else if (this.Y == 5)
            {
                if (this.X == 1)
                {


                }
                else if (this.X == 3)
                {

                }
                else if (this.X == 5)
                {


                }
                else if (this.X == 7)
                {

                }
            }
            else if (this.Y == 6)
            {
                if (this.X == 0)
                {


                }
                else if (this.X == 2)
                {

                }
                else if (this.X == 4)
                {


                }
                else if (this.X == 6)
                {

                }
            }
            else if (this.Y == 7)
            {
                if (this.X == 1)
                {


                }
                else if (this.X == 3)
                {

                }
                else if (this.X == 5)
                {


                }
                else if (this.X == 7)
                {

                }
            }
            */
            throw new NotImplementedException();
        }        
        
        public override KingPiece ToKing()
        {
            return new KingPiece(this);
        }
    }
}
