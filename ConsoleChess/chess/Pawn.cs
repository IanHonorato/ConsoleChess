using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;

namespace chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

        public override bool[,] availableMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0,0);

            if(color == Color.White)
            {
                pos.setValues(position.line - 1, position.column);
                if (board.validPosition(pos) && freePawn(pos))
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line - 2, position.column);
                if (board.validPosition(pos) && freePawn(pos) && nMoves == 0)
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line - 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line - 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                    mat[pos.line, pos.column] = true;
            } 
            else
            {
                pos.setValues(position.line + 1, position.column);
                if (board.validPosition(pos) && freePawn(pos))
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line + 2, position.column);
                if (board.validPosition(pos) && freePawn(pos) && nMoves == 0)
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line + 1, position.column - 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                    mat[pos.line, pos.column] = true;

                pos.setValues(position.line + 1, position.column + 1);
                if (board.validPosition(pos) && hasEnemy(pos))
                    mat[pos.line, pos.column] = true;
            }

            return mat;
        }

        private bool hasEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        public bool freePawn(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override string ToString()
        {
            return "P";
        }

    }
}
