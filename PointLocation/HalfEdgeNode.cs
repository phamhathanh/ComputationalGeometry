﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace PointLocation
{
    public class HalfEdgeNode : Node
    {
        HalfEdge edge;

        public HalfEdgeNode(HalfEdge edge)
        {
            this.edge = edge;
        }
    }
}
