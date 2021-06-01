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
        public bool inCheck { get; private set; }
        public Piece vulnerableEnPassant { get; private set; }

        public GameController() {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            c_pieces = new HashSet<Piece>();
            c_captured = new HashSet<Piece>();
            vulnerableEnPassant = null;
            putPieces();
        }

        //Executa o movimento da peça
        public Piece movePiece(Position origin, Position destiny) {
            Piece p = board.removePiece(origin);
            p.addnMoves();
            Piece capturedPiece =  board.removePiece(destiny);
            board.putPiece(p, destiny);

            if (capturedPiece != null)
                c_captured.Add(capturedPiece);

            //#JogadasEspeciais castles
            if (p is King && destiny.column == origin.column + 2)
            {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinyR = new Position(origin.line, origin.column + 1);
                Piece R = board.removePiece(originR);
                R.addnMoves();
                board.putPiece(R, destinyR);
            }

            //#JogadasEspeciais big castles
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinyR = new Position(origin.line, origin.column - 1);
                Piece R = board.removePiece(originR);
                R.addnMoves();
                board.putPiece(R, destinyR);
            }

            //#JogadaEspecial en passant
            if(p is Pawn)
            {
                if(origin.column != destiny.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                        posP = new Position(destiny.line + 1, destiny.column);
                    else
                        posP = new Position(destiny.line - 1, destiny.column);

                    capturedPiece = board.removePiece(posP);
                    c_captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        //Executa a jogada inteira feita pelo jogador
        public void perfomMove(Position origin, Position destiny) {
            Piece capturedPiece = movePiece(origin, destiny);

            if (hasCheck(currentPlayer))
            {
                rollbackMovement(origin, destiny, capturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            if (hasCheck(opponent(currentPlayer)))
                inCheck = true;
            
            else
                inCheck = false;

            if (checkmateTest(opponent(currentPlayer)))
                finished = true;
            else
                changePlayer();

            Piece p = board.piece(destiny);

            //#JogadaEspecial EnPassant
            if(p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line - 2))
                vulnerableEnPassant = p;
            else
                vulnerableEnPassant = null;  
        }

        public void rollbackMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = board.removePiece(destiny);
            p.remMoves();

            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, destiny);
                c_captured.Remove(capturedPiece);
            }

            board.putPiece(p, origin);

            //#JogadaEspecial castles
            if (p is King && destiny.column == origin.column + 2) {
                Position originR = new Position(origin.line, origin.column + 3);
                Position destinyR = new Position(origin.line, origin.column + 1);

                Piece R = board.removePiece(originR);
                R.addnMoves();

                board.putPiece(R, destinyR);                   
            }

            //#JogadaEspecial big castles
            if (p is King && destiny.column == origin.column - 2)
            {
                Position originR = new Position(origin.line, origin.column - 4);
                Position destinyR = new Position(origin.line, origin.column - 1);

                Piece R = board.removePiece(originR);
                R.addnMoves();

                board.putPiece(R, destinyR);
            }

            //#JogadaEspecial En Passant
            if(p is Pawn)
            {
                if(origin.column != destiny.column && capturedPiece == vulnerableEnPassant)
                {
                    Piece pawn = board.removePiece(destiny);
                    Position posP;
                    
                    if (p.color == Color.White)
                        posP = new Position(3, destiny.column);
                    else
                        posP = new Position(4, destiny.column);

                    board.putPiece(p, posP);
                }
            }
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

        private Color opponent(Color color) {
            if (color == Color.White)
                return Color.Black;
            else
                return Color.White;
        }

        private Piece findKing(Color color)
        {
            foreach(Piece x in inGamePieces(color))
            {
                if (x is King)
                    return x;
            }
            return null;

        }

        public bool hasCheck(Color color)
        {
            Piece king = findKing(color);

            if (king == null)
                throw new BoardException("Não tem rei da cor " + color + " no tabuleiro!");
            
            foreach(Piece x in inGamePieces(opponent(color)))
            {
                bool[,] mat = x.availableMovements();
                if (mat[king.position.line, king.position.column])
                    return true;
            }
            return false;
        }

        public bool checkmateTest(Color color)
        {
            if (!hasCheck(color))
                return false;

            foreach (Piece x in inGamePieces(color))
            {
                bool[,] mat = x.availableMovements();
                for(int i = 0; i < board.lines; i++)
                {
                    for (int j = 0; j < board.columns; j++) {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = movePiece(origin, destiny);
                            bool checkTest = hasCheck(color);
                            rollbackMovement(origin, destiny, capturedPiece);

                            if (!checkTest)
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void putNewPiece(char column, int line, Piece piece) {
            board.putPiece(piece, new ChessPosition(column, line).toPosition());
            c_pieces.Add(piece);
        }

        private void putPieces() {
            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Knight(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Rook(board, Color.Black));
            putNewPiece('b', 8, new Knight(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black, this));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Knight(board, Color.Black));
            putNewPiece('h', 8, new Rook(board, Color.Black));
            putNewPiece('a', 7, new Pawn(board, Color.Black, this));
            putNewPiece('b', 7, new Pawn(board, Color.Black, this));
            putNewPiece('c', 7, new Pawn(board, Color.Black, this));
            putNewPiece('d', 7, new Pawn(board, Color.Black, this));
            putNewPiece('e', 7, new Pawn(board, Color.Black, this));
            putNewPiece('f', 7, new Pawn(board, Color.Black, this));
            putNewPiece('g', 7, new Pawn(board, Color.Black, this));
            putNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
