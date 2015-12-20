using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.Common
{
    public class Segment
    {
        private readonly Vertex vertex1, vertex2;

        public Vertex Vertex1
        {
            get
            {
                return vertex1;
            }
        }

        public Vertex Vertex2
        {
            get
            {
                return vertex2;
            }
        }

        public Segment(Vertex vertex1, Vertex vertex2)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
    }
}
