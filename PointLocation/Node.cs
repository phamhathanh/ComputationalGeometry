using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointLocation
{
    public abstract class Node
    {
        public Node Parent { get; set; }
        public Node LeftChildren { get; set; }
        public Node RightChildren { get; set; }
    }
}
