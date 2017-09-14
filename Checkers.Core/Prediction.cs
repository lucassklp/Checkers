using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    public class Prediction
    {
        public List<Point> LeftPrediction { get; private set; }
        public List<Point> RightPrediction { get; private set; }
    }
}
