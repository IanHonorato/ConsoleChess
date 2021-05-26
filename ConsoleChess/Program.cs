using System;
using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Board board = new Board(8, 8);

            board.putPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.putPiece(new Queen(board, Color.Black), new Position(3, 4));
            board.putPiece(new King(board, Color.Black), new Position(0, 0));
            
            Screen.printBoard(board);

            */
            ChessPosition pos = new ChessPosition('c', 7);
            Console.WriteLine(pos.toPosition());
            Console.WriteLine(pos);

            
            Console.ReadLine();
        }
    }
}
