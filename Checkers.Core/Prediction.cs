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
        public Point LeftPrediction { get; private set; }
        public Point RightPrediction { get; private set; }
    }
}
