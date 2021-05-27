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
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished;

        public GameController() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            putPieces();
        }

        //Executa o movimento da peça
        public void movePiece(Position origin, Position destiny) {
            Piece p = board.removePiece(origin);
            p.addnMoves();
            Piece capturedPiece =  board.removePiece(destiny);
            board.putPiece(p, destiny);
        }

        //Executa a jogada inteira feita pelo jogador
        public void perfomMove(Position origin, Position destiny) {
            movePiece(origin, destiny);
            //turn++;
            changePlayer();
        }

        //Faz a troca dos turnos
        private void changePlayer() {
            if (currentPlayer == Color.White)
                currentPlayer = Color.Black;
            else
            {
                currentPlayer = Color.White;
                turn++;
            }
        }

        public void validateOriginPosition(Position pos) {
            
            if (board.piece(pos) == null)
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            
            if (currentPlayer != board.piece(pos).color)
                throw new BoardException("A peça de origem escolhida não é sua!");
            
            if (!board.piece(pos).hasAvailableMovements())
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
        }

        public void validateDestinyPosition(Position origin, Position destiny)
        {
            if (!board.piece(origin).canMoveTo(destiny))
                throw new BoardException("Posição de destino inválida!");
        }

        private void putPieces() {
            board.putPiece(new Rook(board, Color.Black), new ChessPosition('a', 1).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('c', 4).toPosition());
            board.putPiece(new King(board, Color.Black), new ChessPosition('a', 4).toPosition());
        }
    }
}
