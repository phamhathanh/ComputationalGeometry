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
    public partial class ShortestPath : Form
    {
        /// <summary>
        /// form chạy thuật toán và vẽ hình
        /// </summary>
        public ShortestPath()
        {
            InitializeComponent();
        }
        /*
         * Các biến toàn cục lưu trữ tập các đỉnh của obstacle
         * đỉnh pStart và pEnd
         */
        List<Point2D> m_list_point; // Tập các đỉnh của tập S := {obstacles}
        List<Polygon> m_list_polygon; // Tập các obstacles
        List<Edge> m_list_edge; // Tập các cạnh của tập S := {obstacles}
        Graph m_graph_visibility; // Đồ thị tầm nhìn
        Point2D m_point_pStart; // Đỉnh pStart
        Point2D m_point_pEnd; // Đỉnh pEnd
        Dictionary<Point2D, int> m_dic_pointKey;
        Dictionary<int, Point2D> m_dic_keyPoint;
        Dictionary<Point2D, Edge> m_dic_pointEdge;
        Dictionary<Point2D, Polygon> m_dic_pointPolygon;
        Dictionary<Point2D, bool> m_dic_pointIsVisible;
        int m_i;
        SolidBrush m_sb_polygon;
        Pen m_p_graph;
        Pen m_p_shortestPath;
        Graphics g;
        private void ShortestPath_Load(object sender, EventArgs e)
        {
            /*
             * Khởi tạo các biến toàn cục
             */
            try
            {
                m_dic_pointKey = new Dictionary<Point2D, int>();
                m_dic_keyPoint = new Dictionary<int, Point2D>();
                m_dic_pointEdge = new Dictionary<Point2D, Edge>();
                m_dic_pointPolygon = new Dictionary<Point2D, Polygon>();
                m_list_point = new List<Point2D>();
                m_list_edge = new List<Edge>();
                m_list_polygon = new List<Polygon>();
                m_dic_pointIsVisible = new Dictionary<Point2D, bool>();
                m_i = 0;
                m_sb_polygon = new SolidBrush(Color.DarkBlue);
                m_p_graph = new Pen(Color.Red, 2);
                m_p_shortestPath = new Pen(Color.Yellow, 2);
                g = m_pan_paint.CreateGraphics();
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }
        }

        private void m_cmd_addObstacle_Click(object sender, EventArgs e)
        {
            try
            {
                List<Point2D> v_list_point = new List<Point2D>();
                if (m_grv_toa_do.Rows.Count == 1)
                {
                    MessageBox.Show("Nhập tọa độ!");
                    return;
                }
                for (int i = 0; i < m_grv_toa_do.Rows.Count - 1; i++)
                {
                    double v_doub_x, v_doub_y;
                    if (double.TryParse(m_grv_toa_do.Rows[i].Cells["X"].Value.ToString(), out v_doub_x) && double.TryParse(m_grv_toa_do.Rows[i].Cells["Y"].Value.ToString(), out v_doub_y))
                    {
                        Point2D v_point2D = new Point2D(v_doub_x, v_doub_y);
                        if (m_list_point.Contains(v_point2D))
                        {
                            MessageBox.Show("Tọa độ nhập không hợp lệ! \n(Chỉ xét đa giác là đa giác đơn)");
                            return;
                        }
                        foreach (var item in m_list_polygon)
                        {
                            if (item.BoundaryPolygonContain(v_point2D) || item.Contain(v_point2D))
                            {
                                MessageBox.Show("Tọa độ nhập không hợp lệ! \n(Chỉ xét đa giác là đa giác đơn)");
                                return;
                            }
                        }
                        v_list_point.Add(v_point2D);
                    }
                    else
                    {
                        MessageBox.Show("Sai định dạng tọa độ!");
                        return;
                    }
                }
                Polygon v_polygon = new Polygon(v_list_point);
                m_list_polygon.Add(v_polygon);
                for (int i = 0; i < v_polygon.NodeCount; i++)
                {
                    m_dic_pointPolygon[v_polygon.ListPointOfPolygon[i]] = v_polygon;
                    m_list_point.Add(v_polygon.ListPointOfPolygon[i]);
                    m_dic_keyPoint[m_i] = v_polygon.ListPointOfPolygon[i];
                    m_dic_pointKey[v_polygon.ListPointOfPolygon[i]] = m_i;
                    m_i++;
                    m_dic_pointEdge[v_polygon.ListPointOfPolygon[i]] = new Edge(v_polygon.ListPointOfPolygon[(v_polygon.NodeCount + i - 1) % v_polygon.NodeCount], v_polygon.ListPointOfPolygon[(v_polygon.NodeCount + i + 1) % v_polygon.NodeCount]);
                }
                List<Edge> v_list_edge = v_polygon.GetBoundaryPolygon();
                for (int i = 0; i < v_list_edge.Count; i++)
                {
                    m_list_edge.Add(v_list_edge[i]);
                    int u = m_dic_pointKey[v_list_edge[i].PointStart];
                    int v = m_dic_pointKey[v_list_edge[i].PointEnd];
                }
                DrawPolygon();
                m_grv_toa_do.Rows.Clear();
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }
        }

        private void DrawPolygon()
        {
            foreach (var item in m_list_polygon)
            {
                g.FillPolygon(m_sb_polygon, item.Draw());
            }
        }

        private void DrawEdge(Point2D v_Point2D_1, Point2D v_Point2D_2, Pen v_p)
        {
            g.DrawLines(v_p, new PointF[2] { v_Point2D_1.Draw(), v_Point2D_2.Draw() });
        }

        private void m_cmd_findShortestPath_Click(object sender, EventArgs e)
        {
            /*
             * Tìm shortest path cho đồ thị tầm nhìn
             */
            try
            {
                if (m_point_pStart == m_point_pEnd)
                {
                    MessageBox.Show("Điểm xuất phát và và kết thúc chưa chính xác!");
                    return;
                }
                m_graph_visibility = new Graph(m_list_point.Count); // khởi tạo đồ thị
                foreach (var item in m_list_edge)
                {
                    int u = m_dic_pointKey[item.PointStart];
                    int v = m_dic_pointKey[item.PointEnd];
                    m_graph_visibility[u, v] = item.GetLength();
                    m_graph_visibility[v, u] = m_graph_visibility[u, v];
                }
                // Vẽ đồ thị tầm nhìn
                VisibilityGraphs(m_point_pStart, m_point_pEnd, m_list_point); // hàm trả về đồ thị tầm nhìn m_graph_visibility.
                for (int i = 0; i < m_graph_visibility.LengthNode; i++)
                {
                    for (int j = i + 1; j < m_graph_visibility.LengthNode; j++)
                    {
                        if (m_graph_visibility[i, j] > 0)
                        {
                            DrawEdge(m_dic_keyPoint[i], m_dic_keyPoint[j], m_p_graph);
                        }
                    }
                }
                List<int> v_list_shortestPath = m_graph_visibility.Dijkstra(m_dic_pointKey[m_point_pStart], m_dic_pointKey[m_point_pEnd]);
                for (int i = 0; i < v_list_shortestPath.Count - 1; i++)
                {
                    DrawEdge(m_dic_keyPoint[v_list_shortestPath[i]], m_dic_keyPoint[v_list_shortestPath[i + 1]], m_p_shortestPath);
                }
                m_cmd_addObstacle.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }
        }

        private void VisibilityGraphs(Point2D v_point_pStart, Point2D v_point_pEnd, List<Point2D> v_list_point)
        {
            /*
             * duyệt từng đỉnh v trong S
             * tìm tập các đỉnh trong S được nhìn thấy từ v
             */
            foreach (Point2D item in m_list_point)
            {
                VisiblePoint(item, m_list_point); // Tìm tất cả các đỉnh được nhìn thấy từ v trong S lưu vào đồ thị m_graph_visibility
            }
        }

        private void VisiblePoint(Point2D v_point, List<Point2D> v_list_Point)
        {
            /*
             * sắp xếp các đỉnh theo chiều kim đồng hồ 
             * bắt đầu từ đỉnh gần tia song song với ox nhất
             * Với mỗi tia xuất phát từ item tới 1 đỉnh bất kỳ w trong S
             * Tìm tất các cạnh mà nó cắt
             *  xét đỉnh w có được nhìn thấy hay không
             *  dịch tia tới điểm tiếp theo.
             */
            List<Point2D> v_list_point = new List<Point2D>();
            for (int i = 0; i < v_list_Point.Count; i++)
            {
                v_list_point.Add(v_list_Point[i]);
            }
            v_list_point.Remove(v_point); // Xóa đỉnh v_point ra khỏi S tạm thời

            for (int i = v_list_point.Count - 1; i > 0; i--) // Sắp sắp các đỉnh theo chiều kim đồng hồ 
            {
                bool m_bool_check = false;
                for (int j = 0; j < i; j++)
                {
                    HalfLine v_half_j = new HalfLine(v_point, v_list_point[j]);
                    HalfLine v_half_j_next = new HalfLine(v_point, v_list_point[j + 1]);
                    if (v_half_j.GetClockwiseAngle() < v_half_j_next.GetClockwiseAngle())
                    {
                        Point2D item = v_list_point[j];
                        v_list_point[j] = v_list_point[j + 1];
                        v_list_point[j + 1] = item;
                        m_bool_check = true;
                    }
                    if(Math.Abs(v_half_j.GetClockwiseAngle() - v_half_j_next.GetClockwiseAngle()) < Math.Pow(10,-8))
                    {
                        Edge v_edge_j = new Edge(v_point, v_list_point[j]);
                        Edge v_edge_j_next = new Edge(v_point, v_list_point[j + 1]);
                        if(v_edge_j.GetLength()> v_edge_j_next.GetLength())
                        {
                            Point2D item = v_list_point[j];
                            v_list_point[j] = v_list_point[j + 1];
                            v_list_point[j + 1] = item;
                            m_bool_check = true;
                        }
                    }
                }
                if (m_bool_check == false)
                {
                    break;
                }
            }
            HalfLine v_hafl = new HalfLine(v_point, v_list_point[0]); // Tia xuất phát từ v_point song song với Ox
            List<Edge> v_list_edge = new List<Edge>(); // List các cạnh giao theo thứ tự gần v_point đến xa v_point
            for (int i = 0; i < m_list_edge.Count; i++)
            {
                if (v_hafl.Intersected(m_list_edge[i]))
                {
                    v_list_edge.Add(m_list_edge[i]);
                }
            }
            SortList(ref v_list_edge, v_point);
            for (int i = 0; i < v_list_point.Count; i++)
            {
                if (Visibled(v_list_point[i], v_list_edge, i, v_point, v_list_point))
                {
                    Edge v_edge = new Edge(v_point, v_list_point[i]);
                    m_graph_visibility[m_dic_pointKey[v_point], m_dic_pointKey[v_list_point[i]]] = v_edge.GetLength();
                }
                if (m_dic_pointKey[v_list_point[i]] != m_dic_pointKey[m_point_pStart] && m_dic_pointKey[v_list_point[i]] != m_dic_pointKey[m_point_pEnd])
                {
                    Edge v_edge_i = new Edge(new Point2D(0, 0), new Point2D(v_point.Y - v_list_point[i].Y,v_list_point[i].X- v_point.X));
                    Edge v_edge_start = new Edge(v_list_point[i], m_dic_pointEdge[v_list_point[i]].PointStart);
                    Edge v_edge_end = new Edge(v_list_point[i], m_dic_pointEdge[v_list_point[i]].PointEnd);
                    if(v_edge_i * v_edge_start > 0)
                    {
                        v_list_edge.Add(new Edge(v_list_point[i], m_dic_pointEdge[v_list_point[i]].PointStart));
                    }
                    else
                    {
                        v_list_edge.Remove(new Edge(m_dic_pointEdge[v_list_point[i]].PointStart, v_list_point[i]));
                    }
                    if (v_edge_i * v_edge_end > 0)
                    {
                        v_list_edge.Add(new Edge(v_list_point[i], m_dic_pointEdge[v_list_point[i]].PointEnd));
                    }
                    else
                    {
                        v_list_edge.Remove(new Edge(m_dic_pointEdge[v_list_point[i]].PointEnd, v_list_point[i]));
                    }
                    
                    SortList(ref v_list_edge, v_point);
                }
            }
        }

        private bool Visibled(Point2D v_point2D_i, List<Edge> v_list_edge, int i, Point2D v_point, List<Point2D> v_list_point)
        {
            /*
             * Kiểm tra đỉnh có được nhìn thấy hay không
             */
            Edge v_edge = new Edge(v_point, v_point2D_i);
            if (m_dic_pointKey[v_list_point[i]] != m_dic_pointKey[m_point_pStart] && m_dic_pointKey[v_list_point[i]] != m_dic_pointKey[m_point_pEnd])
            {
                if (v_edge.Intersected(m_dic_pointPolygon[v_point2D_i]))
                {
                    m_dic_pointIsVisible[v_point2D_i] = false;
                    return false;
                }
            }

            if (i == 0 || !v_edge.Contain(v_list_point[i - 1]))
            {
                for (int j = 0; j < v_list_edge.Count; j++)
                {
                    if (v_edge.Intersected(v_list_edge[j]))
                    {
                        m_dic_pointIsVisible[v_point2D_i] = false;
                        return false;
                    }
                }
                m_dic_pointIsVisible[v_point2D_i] = true;
                return true;
            }
            else
            {
                if (m_dic_pointIsVisible[v_list_point[i - 1]] == false)
                {
                    m_dic_pointIsVisible[v_point2D_i] = false;
                    return false;
                }
                else
                {
                    Edge v_edge_i = new Edge(v_list_point[i - 1], v_point2D_i);
                    for (int j = 0; j < v_list_edge.Count; j++)
                    {
                        if (v_edge_i.Intersected(v_list_edge[j]))
                        {
                            m_dic_pointIsVisible[v_point2D_i] = false;
                            return false;
                        }
                    }
                    m_dic_pointIsVisible[v_point2D_i] = true;
                    return true;
                }

            }

        }

        private void SortList(ref List<Edge> v_list_edge, Point2D v_point)
        {
            /*
             * Sắp xếp lại mảng v_list_edge
             */
            for (int i = v_list_edge.Count - 1; i > 0; i--)
            {
                bool v_bol_check = false;
                for (int j = 0; j < i; j++)
                {
                    if (v_point.GetPerpendicular(v_list_edge[j]) > v_point.GetPerpendicular(v_list_edge[j + 1]))
                    {
                        Edge item = v_list_edge[j];
                        v_list_edge[j] = v_list_edge[j + 1];
                        v_list_edge[j + 1] = item;
                        v_bol_check = true;
                    }
                }
                if (v_bol_check == false)
                {
                    break;
                }
            }
        }

        private void m_cmd_clearObstacle_Click(object sender, EventArgs e)
        {
            Clear();
            m_cmd_addObstacle.Enabled = true;
            m_cmd_add_pstart_pend.Enabled = true;
            m_pan_paint.Controls.Clear();
        }
        public void Clear()
        {
            m_list_edge.Clear();
            m_list_point.Clear();
            m_list_polygon.Clear();
            m_dic_keyPoint.Clear();
            m_dic_pointKey.Clear();
            m_dic_pointPolygon.Clear();
            m_dic_pointEdge.Clear();
            m_dic_pointIsVisible.Clear();
            m_txt_pE_x.Text = "";
            m_txt_pE_y.Text = "";
            m_txt_pS_x.Text = "";
            m_txt_pS_y.Text = "";
            m_point_pEnd = new Point2D(0, 0);
            m_point_pStart = new Point2D(0, 0);
            m_i = 0;
            g.Clear(SystemColors.ControlLightLight);
        }
        private void m_txt_pS_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalChar = '.';
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && m_txt_pS_x.Text.IndexOf(decimalChar) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_txt_pS_y_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalChar = '.';
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && m_txt_pS_y.Text.IndexOf(decimalChar) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_txt_pE_x_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalChar = '.';
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && m_txt_pE_x.Text.IndexOf(decimalChar) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_txt_pE_y_KeyPress(object sender, KeyPressEventArgs e)
        {
            char decimalChar = '.';
            if (Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)) { }
            else if (e.KeyChar == decimalChar && m_txt_pE_y.Text.IndexOf(decimalChar) == -1)
            { }
            else
            {
                e.Handled = true;
            }
        }

        private void m_cmd_add_pstart_pend_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_txt_pE_x.Text == "" || m_txt_pS_x.Text == "" || m_txt_pE_y.Text == "" || m_txt_pS_y.Text == "")
                {
                    MessageBox.Show("Nhập điểm bắt đầu và kết thúc");
                    return;
                }
                Point2D v_point_pStart = new Point2D(Convert.ToDouble(m_txt_pS_x.Text), Convert.ToDouble(m_txt_pS_y.Text)); // Khởi tạo pStart;
                Point2D v_point_pEnd = new Point2D(Convert.ToDouble(m_txt_pE_x.Text), Convert.ToDouble(m_txt_pE_y.Text));// Khởi tạo pEnd
                if (v_point_pStart == v_point_pEnd)
                {
                    MessageBox.Show("Điểm xuất phát và và kết thúc chưa chính xác!");
                    return;
                }

                m_point_pStart = v_point_pStart;
                m_list_point.Add(m_point_pStart);
                m_dic_pointKey[m_point_pStart] = m_i;
                m_dic_keyPoint[m_i] = m_point_pStart;
                m_i++;
                m_point_pEnd = v_point_pEnd;
                m_list_point.Add(m_point_pEnd);
                m_dic_pointKey[m_point_pEnd] = m_i;
                m_dic_keyPoint[m_i] = m_point_pEnd;
                m_i++;
                m_cmd_add_pstart_pend.Enabled = false;
                AddLabel(m_point_pStart, m_point_pEnd);
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi!");
            }

        }
        private void AddLabel(Point2D v_pstart,Point2D v_pend)
        {
            Label v_lab_pstart = new Label();
            v_lab_pstart.Text = "P_Start";
            v_lab_pstart.Location = new System.Drawing.Point(Convert.ToInt16(v_pstart.X), Convert.ToInt16(v_pstart.Y));
            m_pan_paint.Controls.Add(v_lab_pstart);
            Label v_lab_pend = new Label();
            v_lab_pend.Text = "P_End";
            v_lab_pend.Location = new System.Drawing.Point(Convert.ToInt16(v_pend.X), Convert.ToInt16(v_pend.Y));
            m_pan_paint.Controls.Add(v_lab_pend);

        }

        private void m_pan_paint_Paint(object sender, PaintEventArgs e)
        {
            DrawPolygon();
        }

    }
}
