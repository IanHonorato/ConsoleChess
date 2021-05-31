using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    //Classe que implementa as peças
    abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int nMoves { get; protected set; }
        public Board board { get; protected set; }

        public Piece( Board board, Color color) {
            this.position = null;
            this.board = board;
            this.color = color;
            this.nMoves = 0;
        }

        public void addnMoves() {
            nMoves++;
        }

        public void remMoves()
        {
            nMoves--;
        }

        public bool hasAvailableMovements() { 
            bool[,] mat = availableMovements();
            for (int i = 0; i < board.lines; i++) {
                for (int j = 0; j < board.columns; j++) {
                    if (mat[i, j])
                        return true;
                }
            }
            return false;
        }

        public bool canMoveTo(Position pos)
        {
            return availableMovements()[pos.line, pos.column];
        }

        public abstract bool[,] availableMovements();
    }
}
