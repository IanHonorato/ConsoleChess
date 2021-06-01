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
        private GameController match;
        public King(Board board, Color color, GameController match) : base(board, color) 
        {
            this.match = match;
        }


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

            
            if(nMoves == 0 && !match.inCheck)
            {
                //#JogadasEspeciais Castles
                Position posR1 = new Position(position.line, position.column + 3);
                if (castlesRookTest(posR1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);

                    if(board.piece(p1) == null && board.piece(p2) == null)
                        mat[position.line, position.column + 2] = true;                  
                }

                //#JogadasEspeciais Big Castles
                Position posR2 = new Position(position.line, position.column - 4);
                if (castlesRookTest(posR2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);

                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                        mat[position.line, position.column - 2] = true;
                }
            }


            return mat;
        }

        private bool castlesRookTest(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.nMoves == 0;
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