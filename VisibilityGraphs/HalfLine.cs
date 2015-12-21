using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityGraphs
{
    struct HalfLine
    {
        /// <summary>
        /// struct tia xuất phát từ một đỉnh pStart tới pEnd
        /// </summary>
        private Point2D m_point_pStart;
        private Point2D m_point_pEnd;

        public HalfLine(Point2D v_point_pStart,Point2D v_point_pEnd)
        {
            m_point_pStart = v_point_pStart;
            m_point_pEnd = v_point_pEnd;
        }

        public Point2D P_Start
        {
            get
            {
                return m_point_pStart;
            }
        }

        public Point2D P_End
        {
            get
            {
                return m_point_pEnd;
            }
        }

        public double GetClockwiseAngle()
        {
            Edge v_edge_parallelOx = new Edge(m_point_pStart, new Point2D(m_point_pStart.X + 1, m_point_pStart.Y));
            Edge v_edge_hafl = new Edge(m_point_pStart, m_point_pEnd);
            double v_doub_cos = Edge.Cos(v_edge_parallelOx, v_edge_hafl);
            if (m_point_pEnd.Y >= m_point_pStart.Y)
                return Math.PI * 2 - Math.Acos(v_doub_cos);
            return Math.Acos(v_doub_cos);
        }

        public bool Intersected(Edge v_edge)
        {
            Edge v_edge_pStart = new Edge(m_point_pStart, v_edge.PointStart);
            Edge v_edge_pEnd = new Edge(m_point_pStart, v_edge.PointEnd);
            Edge v_edge_half = new Edge(m_point_pStart, m_point_pEnd);
            if (Edge.Cos(v_edge_pStart, v_edge_half) > Edge.Cos(v_edge_pStart, v_edge_pEnd) && Edge.Cos(v_edge_pEnd, v_edge_half) > Edge.Cos(v_edge_pStart, v_edge_pEnd) && (Edge.Cos(v_edge_pStart, v_edge_half) > 0 || Edge.Cos(v_edge_pEnd, v_edge_half) > 0))
            {
                return true;
            }
            return false;
        }
        
        public bool Contain(Point2D v_point)
        {
            Edge v_edge_1 = new Edge(v_point, m_point_pEnd);
            Edge v_edge_2 = new Edge(m_point_pStart, v_point);
            if(Math.Abs(v_edge_1*v_edge_2 +v_edge_1.GetLength()*v_edge_2.GetLength()) < Math.Pow(10,-8) )
            {
                return true;
            }
            return false;
        }
    }
}
