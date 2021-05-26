using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using board;

namespace chess
{
    class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color) { }

        public override bool[,] availableMovements()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "D";
        }
    }
}
