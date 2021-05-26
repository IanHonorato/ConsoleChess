using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;
namespace chess
{
    class GameController
    {
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
        public bool finished;

        public GameController() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces();
        }

        public void movePiece(Position origin, Position destiny) {
            Piece p = board.removePiece(origin);
            p.addnMoves();
            Piece capturedPiece =  board.removePiece(destiny);
            board.putPiece(p, destiny);
        }

        private void putPieces() {
            board.putPiece(new Rook(board, Color.Black), new ChessPosition('a', 1).toPosition());
            board.putPiece(new Queen(board, Color.White), new ChessPosition('c', 4).toPosition());
            board.putPiece(new King(board, Color.Black), new ChessPosition('a', 4).toPosition());
        }
    }
}
