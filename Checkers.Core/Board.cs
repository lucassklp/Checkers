using Checkers.Core.Enums;
using Checkers.Core.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    public class Board
    {
        private const int BOARD_SIZE = 8;

        public List<Piece> Pieces { get; private set; }

        public Board()
        {
            this.Pieces = new List<Piece>();

            //Monta e posiciona as peças no tabuleiro
            this.BuildPieces();
        }

        private void BuildPieces()
        {
            //Constrói as peças vermelhas
            for (int row = 0; row < 3; row++)
            {
                for (int column = row % 2; column < BOARD_SIZE; column += 2)
                {
                    this.Pieces.Add(new RedPiece(row, column));
                }
            }

            //Constrói as peças pretas
            for (int row = BOARD_SIZE - 3; row < BOARD_SIZE; row++)
            {
                for (int column = row % 2; column < BOARD_SIZE; column += 2)
                {
                    this.Pieces.Add(new BlackPiece(row, column));
                }
            }
        }

        public void PredictMoviment(int x, int y)
        {
            var piece = this.Pieces.Find(p => p.X == x && p.Y == y).Predict(this);
        }

    }
}
