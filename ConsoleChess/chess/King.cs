using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;

namespace chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }


        //Checa todos os movimentos possíveis pela peça
        public override bool[,] availableMovements()
        {
            bool [,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            //acima
            pos.setValues(position.line - 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //nordeste
            pos.setValues(position.line - 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //direita
            pos.setValues(position.line, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //sudeste
            pos.setValues(position.line + 1, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //abaixo
            pos.setValues(position.line + 1, position.column);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //sudoeste
            pos.setValues(position.line + 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //esquerda
            pos.setValues(position.line, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            //noroeste
            pos.setValues(position.line - 1, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            return mat;
        }

        private bool canMove(Position pos) {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}