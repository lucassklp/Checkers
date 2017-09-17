using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Core
{
    [Serializable]
    public class Prediction
    {
        public List<Point> LeftPrediction { get; private set; }
        public List<Point> RightPrediction { get; private set; }

        public Prediction()
        {
            this.LeftPrediction = new List<Point>();
            this.RightPrediction = new List<Point>();
        }
    }
}
