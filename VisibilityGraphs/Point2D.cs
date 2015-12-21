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
    struct Point2D
    {
        /// <summary>
        /// struct điểm trong không gian 2 chiều
        /// với tọa độ X,Y
        /// </summary>
        private double m_doub_x;
        private double m_doub_y;
        public Point2D(double v_doub_x, double v_doub_y)
        {
            m_doub_x = v_doub_x;
            m_doub_y = v_doub_y;
        }

        public double X // Tọa độ X
        {
            get
            {
                return m_doub_x;
            }
        }

        public double Y // Tọa độ Y
        {
            get
            {
                return m_doub_y;
            }
        }

        public double GetPerpendicular(Edge v_edge)
        {
            /*
             * Tính khoảng cách từ 1 đỉnh tới 1 đoạn thẳng
             * Dùng công thức heron
             */
            Edge v_edgeStart = new Edge(this, v_edge.PointStart);
            Edge v_edgeEnd = new Edge(this, v_edge.PointEnd);
            double v_doub_a = v_edgeStart.GetLength();
            double v_doub_b = v_edgeEnd.GetLength();
            double v_doub_c = v_edge.GetLength();
            double v_doub_p = (v_doub_a + v_doub_b + v_doub_c) / 2;
            return (2 * Math.Sqrt(v_doub_p * (v_doub_p - v_doub_a) * (v_doub_p - v_doub_b) * (v_doub_p - v_doub_c)) / v_doub_c);

        }

        public PointF Draw()
        {
            float x, y;
            x = float.Parse(this.X.ToString());
            y = float.Parse(this.Y.ToString());
            return new PointF(x, y);
        }
        public static bool operator == (Point2D v_point1 , Point2D v_point2)
        {
            if (v_point1.X == v_point2.X && v_point1.Y == v_point2.Y)
                return true;
            else
                return false;
        }
        public static bool operator !=(Point2D v_point1, Point2D v_point2)
        {
            if (v_point1 == v_point2)
                return false;
            else
                return true ;
        }
    }
}
