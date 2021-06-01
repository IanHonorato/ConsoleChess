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
        private GameController match;
        public Pawn(Board board, Color color, GameController match) : base(board, color) {
            this.match = match;
        }

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

                //#JogadaEspecial En Passant
                if (position.line == 3)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line - 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line - 1, right.column] = true;
                    }
                }
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

                if (position.line == 4)
                {
                    Position left = new Position(position.line, position.column - 1);
                    if (board.validPosition(left) && hasEnemy(left) && board.piece(left) == match.vulnerableEnPassant)
                    {
                        mat[left.line + 1, left.column] = true;
                    }

                    Position right = new Position(position.line, position.column + 1);
                    if (board.validPosition(right) && hasEnemy(right) && board.piece(right) == match.vulnerableEnPassant)
                    {
                        mat[right.line + 1, right.column] = true;
                    }
                }
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
