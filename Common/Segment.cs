using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.Common
{
    public class Segment
    {
        private readonly Vector2 vertex1, vertex2;

        public Vector2 Vertex1
        {
            get
            {
                return vertex1;
            }
        }

        public Vector2 Vertex2
        {
            get
            {
                return vertex2;
            }
        }

        public Segment(Vector2 vertex1, Vector2 vertex2)
        {
            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
        }
    }
}
