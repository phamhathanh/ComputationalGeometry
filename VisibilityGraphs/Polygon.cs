using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisibilityGraphs
{
    struct Polygon
    {
        /// <summary>
        /// Lớp đa giác 
        /// </summary>
        private List<Point2D> m_list_node ;
        public Polygon(List<Point2D> v_list_node)
        {
            m_list_node = v_list_node;
        }
        public List<Point2D> ListPointOfPolygon
        {
            get
            {
                return m_list_node;
            }
        }
        public int NodeCount // trả về số đỉnh của đa giác
        {
            get
            {
                return m_list_node.Count;
            }
        }
        public void Add(Point2D v_node)
        {
            m_list_node.Add(v_node);
        }
        /*
         * Lấy các cạnh của đa giác
         */
        public List<Edge> GetBoundaryPolygon()
        {
            List<Edge> v_list_boundaryEdge = new List<Edge>();
            for (int i = 0; i < NodeCount; i++)
            {
                v_list_boundaryEdge.Add(new Edge(m_list_node[i % NodeCount], m_list_node[(i + 1) % NodeCount]));
            }
            return v_list_boundaryEdge;
        }
        public bool BoundaryPolygonContain(Point2D v_point)
        {
            List<Edge> v_list_boundaryEdge = GetBoundaryPolygon();
            for (int i = 0; i < v_list_boundaryEdge.Count; i++)
            {
                if(v_list_boundaryEdge[i].Contain(v_point))
                {
                    return true;
                }
            }
            return false;
        }
        public bool Contain(Point2D v_point)
        {
            List<Edge> v_list_boundaryEdge = GetBoundaryPolygon();
            for (int i = 0; i < m_list_node.Count; i++)
            {
                int v_int_dem = 0;
                HalfLine v_hafl = new HalfLine(m_list_node[i], v_point);
                foreach (var item in v_list_boundaryEdge)
                {
                    if (!v_hafl.Intersected(item))
                    {
                        v_int_dem++;
                    }
                }
                if(v_int_dem  == v_list_boundaryEdge.Count)
                {
                    return false;
                }
            }
            return true;
        }

        public PointF[] Draw()
        {
            PointF[] v_arr_pointF = new PointF[this.NodeCount];
            for (int i = 0; i < this.NodeCount; i++)
            {
                v_arr_pointF[i] = m_list_node[i].Draw();
            }
            return v_arr_pointF;
        }
    }
}
