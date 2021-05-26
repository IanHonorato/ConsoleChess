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

        //imprime tabuleiro na tela
        public static void printBoard(Board board) {
            for (int i = 0; i < board.lines; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++) {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("  a b c d e f g h");
        }

        //imprime tabuleiro na tela com as posições possíveis
        public static void printBoard(Board board, bool[,] availablePositions)
        {
            ConsoleColor originBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (availablePositions[i, j])                    
                        Console.BackgroundColor = newBackground;                    
                    else                    
                        Console.BackgroundColor = originBackground;            
                    
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = originBackground;
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = originBackground;
            Console.WriteLine("  a b c d e f g h");
        }

        //Faz a leitura da entrada do usuário para mover as peças
        public static ChessPosition readChessPosition() {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");

            return new ChessPosition(column, line);
        }

        //imprime a peça dentro do tabuleiro
        public static void printPiece(Piece piece) {
            if (piece == null)
                Console.Write("- ");
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else if (piece.color == Color.Black)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
