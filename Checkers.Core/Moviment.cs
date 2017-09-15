using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    [Serializable]
    public class Moviment
    {
        public Point Destination { get; private set; }

        public Moviment(Point dest)
        {
            this.Destination = dest;
        }
    }
}
