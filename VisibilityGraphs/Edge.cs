using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityGraphs
{
    struct Edge
    {
        /// <summary>
        /// struct "cạnh" trong không gian 2 chiều
        /// </summary>
        private Point2D m_pointStart;

        private Point2D m_pointEnd;

        public Edge(Point2D v_pointStart, Point2D v_pointEnd)
        {
            m_pointStart = v_pointStart;
            m_pointEnd = v_pointEnd;
        }

        public Point2D PointStart
        {
            get
            {
                return m_pointStart;
            }
        }

        public Point2D PointEnd
        {
            get
            {
                return m_pointEnd;
            }
        }

        public double GetLength()
        {
            return Math.Sqrt((m_pointEnd.X - m_pointStart.X) * (m_pointEnd.X - m_pointStart.X) + (m_pointEnd.Y - m_pointStart.Y) * (m_pointEnd.Y - m_pointStart.Y));
        }

        public static double Cos(Edge v_edge1, Edge v_edge2)
        {
            return (v_edge1 * v_edge2) / (v_edge1.GetLength() * v_edge2.GetLength());
        }

        public static double operator *(Edge v_edge1, Edge v_edge2)
        {
            return (v_edge1.PointEnd.X - v_edge1.PointStart.X) * (v_edge2.PointEnd.X - v_edge2.PointStart.X) + (v_edge1.PointEnd.Y - v_edge1.PointStart.Y) * (v_edge2.PointEnd.Y - v_edge2.PointStart.Y);
        }

        public bool Contain(Point2D v_point)
        {
            Edge v_edge_1 = new Edge(v_point, m_pointEnd);
            Edge v_edge_2 = new Edge(m_pointStart, v_point);
            if (Math.Abs(v_edge_1 * v_edge_2 + v_edge_1.GetLength() * v_edge_2.GetLength()) < Math.Pow(10, -8))
            {
                return true;
            }
            return false;
        }

        public bool Intersected(Edge v_edge)
        {
            HalfLine v_hafl_s_e = new HalfLine(m_pointStart, m_pointEnd);
            HalfLine v_hafl_e_s = new HalfLine(m_pointEnd, m_pointStart);
            if (v_hafl_s_e.Intersected(v_edge) && v_hafl_e_s.Intersected(v_edge))
            {
                return true;
            }
            else
                return false;
        }

        public bool Intersected(Polygon v_polygon)
        {
            List<Point2D> v_point_polygon = v_polygon.ListPointOfPolygon;
            for (int i = 0; i < v_point_polygon.Count; i++)
            {
                for (int j = i+1; j < v_point_polygon.Count; j++)
                {
                    Edge v_edge = new Edge(v_point_polygon[i], v_point_polygon[j]);
                    if(this.Intersected(v_edge))
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }
        public static bool operator ==(Edge v_edge1, Edge v_edge2)
        {
            if ((v_edge1.PointStart == v_edge2.PointStart && v_edge1.PointEnd == v_edge2.PointEnd) || (v_edge1.PointStart == v_edge2.PointEnd && v_edge1.PointEnd == v_edge2.PointStart))
                return true;
            else return false;
        }
        public static bool operator !=(Edge v_edge1, Edge v_edge2)
        {
            if (v_edge1 == v_edge2)
                return false;
            else return true;
        }
    }
}
