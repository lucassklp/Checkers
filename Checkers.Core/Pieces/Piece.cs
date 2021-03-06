﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core.Pieces
{
    [Serializable]
    public abstract class Piece
    {
        public int X { get; protected set; }
        public int Y { get; protected set; }

        public Piece(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }


        public bool CanEat(Board board)
        {
            var prediction = PredictToEat(board);
            // Verifica se existe predicts para comer, caso verdadeiro, pode comer.
            if (prediction.Predictions.Count() != 0)
                return true;
            else
                return false;
        }

        public bool IsMovimentValid(Board board, Point point)
        {
            var prediction = this.GetPredictions(board);
            return prediction.Predictions.Exists(mv => mv.X == point.X && mv.Y == point.Y);
        }


        /// <summary>
        /// Move a piece on the board
        /// </summary>
        /// <param name="Board">O tabuleiro</param>
        /// <param name="moviment">O movimento que essa peça fará</param>
        /// <returns>Se a peça comeu</returns>
        public bool Move(Board Board, Point moviment)
        {
            bool eaten = false;

            //Lógica para comer a peça
            if(Math.Abs(this.X - moviment.X) > 1)
            {
                eaten = Board.Eat(this, moviment);
            }
             
            //Efetua o movimento
            this.X = moviment.X;
            this.Y = moviment.Y;

            
            //Checa se a peça se transformará numa dama, caso positivo, essa peça se transformará
            if (this.TransformToKing())
            {
                Board.SetKing(this);
            }

            return eaten;
        }

        public Prediction GetPredictions(Board board)
        {
            var allPredictions = this.Predict(board);
            allPredictions.Predictions.AddRange(this.PredictToEat(board).Predictions);
            return allPredictions;
        }
        public abstract bool TransformToKing();

        public abstract Prediction Predict(Board Board);

        public abstract Prediction PredictToEat(Board board);

        public abstract KingPiece ToKing();

        public abstract bool IsEnemyPiece(Piece p);
    }
}
