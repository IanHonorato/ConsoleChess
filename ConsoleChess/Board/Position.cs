using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    //Classe que implementa o sistema de posicionamento das peças.
    class Position
    {
        public int line { get; set; }
        public int column { get; set; }

        public Position(int line, int column) {
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return line 
                + ", "
                + column;
        }
    }
}
