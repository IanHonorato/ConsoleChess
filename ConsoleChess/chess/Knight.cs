using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;

namespace chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color) { }

        public override bool[,] availableMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            pos.setValues(position.line - 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line - 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line - 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line - 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line + 1, position.column + 2);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line + 2, position.column + 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line + 2, position.column - 1);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            pos.setValues(position.line + 1, position.column - 2);
            if (board.validPosition(pos) && canMove(pos))
                mat[pos.line, pos.column] = true;

            return mat;
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}