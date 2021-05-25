using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    //Classe estrutural do tabuleiro
    class Board
    {
        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns) {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        //Retorna a peça que está na posição definida nos parâmetros
        public Piece piece(int line, int column) {
            return pieces[line, column];
        }

        //Retorna a peça que está na posição definida nos parâmetros
        public Piece piece(Position pos) {
            return pieces[pos.line, pos.column];
        }

        public bool hasPiece(Position pos) {
            validatePosition(pos);
            return piece(pos) != null;
        }

        //Coloca a peça no tabuleiro
        public void putPiece(Piece p, Position pos) {
            if (hasPiece(pos)) 
                throw new BoardException("Já existe uma peça nessa posição");
            
            pieces[pos.line, pos.column] = p;
            p.position = pos;
        }

        //Verifica se a posição não excede os limites do tabuleiro
        public bool validPosition(Position pos) {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns)
                return false;
            else
                return true;
        }

        
        //Lança a exceção para a validação de posição
        public void validatePosition(Position pos) {
            if (!validPosition(pos))
                throw new BoardException("Posição inválida!");
        }
    }
}
