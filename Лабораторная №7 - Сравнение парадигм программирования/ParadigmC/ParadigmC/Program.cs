using System;
using System.IO;
using System.Collections.Generic;

namespace ParadigmC
{
    class Program
    {
        struct edge
        {
            public int first_vertex,
                second_vertex;
        }
        struct read_edge
        {
            public int first, second;
            public int value;
        }
        static void Main(string[] args)
        {
            long ellapledTicks = DateTime.Now.Ticks;
            int len;
            List < read_edge > edges = new List<read_edge>();
            using (var reader = new StreamReader("input.txt"))
            {
                len = int.Parse(reader.ReadLine());
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    read_edge temp;
                    string[] temp_l = line.Split(' ');
                    temp.first = int.Parse(temp_l[0]);
                    temp.second = int.Parse(temp_l[1]); 
                    temp.value = int.Parse(temp_l[2]);
                    edges.Add(temp);
                }
            }
            int[,] matrix = new int[len, len];
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < len; j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else matrix[i, j] = Int32.MaxValue;
                }
            }
            for (int i = 0; i < edges.Count; i++)
            {
                matrix[edges[i].first, edges[i].second] = edges[i].value;
                matrix[edges[i].second, edges[i].first] = edges[i].value;
            }
            kruscal(matrix, len);

            ellapledTicks = DateTime.Now.Ticks - ellapledTicks;
            Console.WriteLine((new TimeSpan(ellapledTicks)).TotalMilliseconds + "мс");
        }
        public static void kruscal(int[,] matrix, int len)
        {
            string temp;
            edge new_edge;
            new_edge.first_vertex = 0;
            new_edge.second_vertex = 0;
            int totalweight = 0,
                edge_done = 0,
                currrent_min;
            List<string> outputs = new List<string>();
            //Создание коллекции предков вершин, то есть связных с ними вершин
            List<int> ancestors = new List<int>();
            for (int i = 0; i < len; i++)
            {
                //В начальный момент ни одна вершина не имеет предков, поэтому записываем что вершина является своим наследником
                ancestors.Add(i);
            }

            //Нам нужно создать m = n - 1 дуг, которые свяжут все вершины
            while (edge_done < len - 1)
            {
                currrent_min = Int32.MaxValue;
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < len; j++)
                    {
                        //Вся матрица не сортируется отдельным циклом, здесь каждый раз ищется дуга с минимальным весом
                        //loop_check проверяет, не создаст ли данная дуга цикл
                        if (matrix[i,j] < currrent_min && loop_check(ancestors, i) != loop_check(ancestors, j))
                        {
                            currrent_min = matrix[i,j];
                            new_edge.first_vertex = i;
                            new_edge.second_vertex = j;
                        }
                    }
                }
                ancestors[loop_check(ancestors, new_edge.first_vertex)] = loop_check(ancestors, new_edge.second_vertex);
                temp = (new_edge.first_vertex + 1).ToString() + ' ' + (new_edge.second_vertex + 1).ToString();
                outputs.Add(temp);
                totalweight += currrent_min;
                edge_done++;
            }
            Console.WriteLine(totalweight);
        }
        public static int loop_check(List<int> ancestors, int vertex)
        {
            while (ancestors[vertex] != vertex) vertex = ancestors[vertex];
            return vertex;
        }

    }
}
