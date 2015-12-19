using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace ComputationalGeometry.TrapezoidalMap
{
    class VertexNode: Node
    {
        Vertex Vertex;

        public VertexNode(Vertex vertex)
        {
            this.Vertex = vertex;
        }
    }
}
