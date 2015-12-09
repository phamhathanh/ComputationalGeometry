using ComputationGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointLocation
{
    public class TrapezoidalNode : Node
    {
        Trapezoidal Trapezoidal;

        public TrapezoidalNode(Trapezoidal trapezoidal)
        {
            this.Trapezoidal = trapezoidal;
            LeftChildren = null;
            RightChildren = null;
        }
    }
}
