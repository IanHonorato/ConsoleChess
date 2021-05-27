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
                try
                {
                    Console.Clear();
                    Screen.printMatch(match);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Screen.readChessPosition().toPosition();
                    match.validateOriginPosition(origin);


                    bool[,] availablePositions = match.board.piece(origin).availableMovements();

                    Console.Clear();
                    Screen.printBoard(match.board, availablePositions);

                    Console.WriteLine();
                    Console.Write("Destino: ");
                    Position destiny = Screen.readChessPosition().toPosition();
                    match.validateDestinyPosition(origin, destiny);


                    match.perfomMove(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            //} catch {

            //}  
            Console.ReadLine();
        }
    }
}
