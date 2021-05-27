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
        private HashSet<Piece> c_pieces;
        private HashSet<Piece> c_captured;

        public GameController() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            c_pieces = new HashSet<Piece>();
            c_captured = new HashSet<Piece>();
            putPieces();
        }

        //Executa o movimento da peça
        public void movePiece(Position origin, Position destiny) {
            Piece p = board.removePiece(origin);
            p.addnMoves();
            Piece capturedPiece =  board.removePiece(destiny);
            board.putPiece(p, destiny);

            if (capturedPiece != null)
                c_captured.Add(capturedPiece);
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

        public HashSet<Piece> capturedPieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in c_captured)
            {
                if (x.color == color)
                    aux.Add(x);
            }
            return aux;
        }

        public HashSet<Piece> inGamePieces(Color color) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in c_pieces)
            {
                if (x.color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void putNewPiece(char column, int line, Piece piece) {
            board.putPiece(piece, new ChessPosition(column, line).toPosition());
            c_pieces.Add(piece);
        }

        private void putPieces() {
            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('d', 1, new King(board, Color.White));
            putNewPiece('e', 1, new Rook(board, Color.White));
            putNewPiece('c', 1, new Rook(board, Color.White));


            putNewPiece('a', 8, new Rook(board, Color.Black));
            putNewPiece('h', 8, new Rook(board, Color.Black));
            putNewPiece('d', 8, new King(board, Color.Black));
            putNewPiece('e', 8, new Rook(board, Color.Black));
            putNewPiece('c', 8, new Rook(board, Color.Black));
        }
    }
}
