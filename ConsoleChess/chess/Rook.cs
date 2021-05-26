using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;

namespace chess
{
    class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }

        //Checa todos os movimentos possíveis pela peça
        public override bool[,] availableMovements()
        {
            bool[,] mat = new bool[board.lines, board.columns];

            Position pos = new Position(0, 0);

            //acima
            pos.setValues(position.line - 1, position.column);
            while (board.validPosition(pos) && canMove(pos)) {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;

                pos.line--;
            }

            //abaixo
            pos.setValues(position.line + 1, position.column);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;

                pos.line++;
            }

            //direita
            pos.setValues(position.line, position.column + 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;

                pos.column++;
            }

            //esquerda
            pos.setValues(position.line, position.column - 1);
            while (board.validPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                    break;

                pos.column--;
            }


            return mat;
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}
