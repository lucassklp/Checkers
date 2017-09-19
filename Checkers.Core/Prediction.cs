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
        public List<Point> Predictions { get; private set; }

        public Prediction()
        {
            this.Predictions = new List<Point>();
        }
    }
}
