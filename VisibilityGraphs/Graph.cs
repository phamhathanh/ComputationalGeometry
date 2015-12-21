using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisibilityGraphs
{
    class Graph
    {
        private double[,] m_double_matrix;

        public Graph(int v_int_LenNode)
        {
            m_double_matrix = new double[v_int_LenNode, v_int_LenNode];
        }

        public Graph(double[,] v_double_matrix)
        {
            m_double_matrix = v_double_matrix;
        }

        public double this[int i,int j]
        {
            get
            {
                return GetAt(i, j);
            }
            set
            {
                SetAt(i, j, value);
            }
        }

        private void SetAt(int i, int j, double value)
        {
            if (i == j)
                m_double_matrix[i, j] = 0;
            else
                m_double_matrix[i, j] = value;
        }

        private double GetAt(int i, int j)
        {
            if (i == j)
                return 0;
                
            else
            {
                if (i != j && m_double_matrix[i, j] !=0)
                    return m_double_matrix[i, j];
                else
                    return -1;
            }

        }

        public int LengthNode
        {
            get
            {
                return m_double_matrix.GetLength(1);
            }
        }

        public List<int> Dijkstra(int v_int_start,int v_int_end)
        {
            List<int> v_int_T = new List<int>();
            double[] v_double_D = new double[LengthNode];
            int[] v_int_pre = new int[LengthNode];
            for (int i = 0; i < LengthNode; i++)
            {
                v_int_T.Add(i);
                v_double_D[i] = this[v_int_start, i];
                v_int_pre[i] = v_int_start; 
            }
            v_int_T.Remove(v_int_start);
            while(v_int_T.Count > 0)
            {
                double v_double_min = v_double_D[v_int_T[0]];
                int v_int_u=0;
                foreach (var item in v_int_T)
                {
                    if (v_double_min < 0 && v_double_D[item] > 0)
                    {
                        v_double_min = v_double_D[item];
                        v_int_u = item;
                    }
                    if (v_double_min >= v_double_D[item] && v_double_D[item] > 0)
                    {
                        v_double_min = v_double_D[item];
                        v_int_u = item;
                    }
                }
                v_int_T.Remove(v_int_u);
                foreach (var item in v_int_T)
                {
                    if ((v_double_D[item] < 0 && this[v_int_u, item] > 0) || (this[v_int_u, item] > 0 && v_double_D[item] > v_double_D[v_int_u] + this[v_int_u, item])) 
                    {
                        v_double_D[item] = v_double_D[v_int_u] + this[v_int_u, item];
                        v_int_pre[item] = v_int_u;
                    }
                }

            }
            List<int> v_int_result = new List<int>();
            v_int_result.Add(v_int_end);
            while (v_int_result[v_int_result.Count - 1] != v_int_start)
            {
                v_int_result.Add(v_int_pre[v_int_result[v_int_result.Count - 1]]);
            }
            return v_int_result;
        }

    }
}
