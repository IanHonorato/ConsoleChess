using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;
using chess;

namespace ConsoleChess
{
    class Screen
    {

        public static void printBoard(Board board) {
            for (int i = 0; i < board.lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++) {
                    if (board.piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        printPiece(board.piece(i, j));
                    }
                }

                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static ChessPosition readChessPosition() {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");

            return new ChessPosition(column, line);
        }

        public static void printPiece(Piece piece) {
            if (piece.color == Color.White)
            {
                Console.Write(piece);
            }
            else if (piece.color == Color.Black) {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }
    }
}
