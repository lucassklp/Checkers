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
    public class Board
    {
        private const int BOARD_SIZE = 8;

        public List<Piece> WhitePieces { get; set; }
        public List<Piece> RedPieces { get; set; }


        public Board()
        {
            this.WhitePieces = new List<Piece>();
            this.RedPieces = new List<Piece>();
        }


        public void Eat(Piece movimentingPiece, Point destination)
        {
            //Come todo mundo q aparecer no caminho


            //A peça está se movimentando para cima
            if(destination.X < movimentingPiece.X)
            {
                //A peça está se movendo para esquerda
                if(destination.Y < movimentingPiece.Y)
                {
                    int x = movimentingPiece.X - 1;
                    int y = movimentingPiece.Y - 1;
                    for (; x != destination.X && y != destination.Y; x--, y--)
                    {
                        if(this[x, y] != null) //Peças no caminho... Remover...
                        {
                            var eatenPiece = this[x, y];
                            this.EatPiece(eatenPiece);
                        }
                    }
                }
                else //A peça está se movendo para a direita
                {
                    int x = movimentingPiece.X - 1;
                    int y = movimentingPiece.Y + 1;
                    for (; x != destination.X && y != destination.Y; x--, y++)
                    {
                        if (this[x, y] != null) //Peças no caminho... Remover...
                        {
                            var eatenPiece = this[x, y];
                            this.EatPiece(eatenPiece);
                        }
                    }
                }
            }
            else //A peça está se movendo para baixo
            {
                //A peça está se movendo para esquerda
                if (destination.Y < movimentingPiece.Y)
                {
                    int x = movimentingPiece.X + 1;
                    int y = movimentingPiece.Y - 1;
                    for (; x != destination.X && y != destination.Y; x++, y--)
                    {
                        if (this[x, y] != null) //Peças no caminho... Remover...
                        {
                            var eatenPiece = this[x, y];
                            this.EatPiece(eatenPiece);
                        }
                    }
                }
                else //A peça está se movendo para a direita
                {
                    int x = movimentingPiece.X + 1;
                    int y = movimentingPiece.Y + 1;
                    for (; x != destination.X && y != destination.Y; x++, y++)
                    {
                        if (this[x, y] != null) //Peças no caminho... Remover...
                        {
                            var eatenPiece = this[x, y];
                            this.EatPiece(eatenPiece);
                        }
                    }
                }
            }
        }


        private void EatPiece(Piece p)
        {
            if (this.RedPieces.Contains(p))
                this.RedPieces.Remove(p);
            else this.WhitePieces.Remove(p);
        }

        public void SetKing(Piece p)
        {
            var king = p.ToKing();
            if(this.RedPieces.Exists(piece => piece.X == king.X && piece.Y == king.Y))
            {
                var piece = this.RedPieces.Find(pi => pi.X == king.X && pi.Y == king.Y);
                var index = this.RedPieces.IndexOf(piece);
                this.RedPieces[index] = king;
            }
            else
            {
                var piece = this.WhitePieces.Find(pi => pi.X == king.X && pi.Y == king.Y);
                var index = this.WhitePieces.IndexOf(piece);
                this.WhitePieces[index] = king;
            }
        }

        public Piece this[int x, int y]
        {
            get
            {
                var pesq1 = this.WhitePieces.Find(piece => piece.X == x && piece.Y == y);
                if(pesq1 == null)
                {
                    return this.RedPieces.Find(piece => piece.X == x && piece.Y == y);
                }
                return pesq1;
            }
            
        }

    }
}
