using System;
using board;
using chess;

namespace ConsoleChess
{
    class Program
    {
        static void Main(string[] args)
        {
            //try { 

            GameController match = new GameController();

            while (!match.finished) {

                Console.Clear();
                Screen.printBoard(match.board);

                Console.WriteLine();
                Console.Write("Origem: ");
                Position origin = Screen.readChessPosition().toPosition();

                bool[,] availablePositions = match.board.piece(origin).availableMovements();

                Console.Clear();
                Screen.printBoard(match.board, availablePositions);

                Console.WriteLine();
                Console.Write("Destino: ");
                Position destiny = Screen.readChessPosition().toPosition();

                match.movePiece(origin, destiny);
            }

            //} catch {

            //}  
            Console.ReadLine();
        }
    }
}
