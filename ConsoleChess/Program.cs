using System;
using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            GameController match = new GameController();

            while (!match.finished) {

                Console.Clear();
                Screen.printBoard(match.board);

                Console.WriteLine();
                Console.Write("Origem: ");
                Position origin = Screen.readChessPosition().toPosition();
                Console.Write("Destino: ");
                Position destiny = Screen.readChessPosition().toPosition();

                match.movePiece(origin, destiny);
            }

            
            //ChessPosition pos = new ChessPosition('c', 7);
            //Console.WriteLine(pos.toPosition());
            //Console.WriteLine(pos);

            
            Console.ReadLine();
        }
    }
}
