using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class NeuralData
    {
        public double[][] Data;
        public int Row = 0;

        public NeuralData(int rows)
        {
            Data = new double[rows][];
        }

        public void Add(DataTable dataTable)
        {
            List<double> data = new List<double>();
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dr = dataTable.Rows[i];
                foreach (double item in dr.ItemArray)
                {
                    data.Add(item);
                }
                Data[i] = data.ToArray();
                data.Clear();
            }
            
        }
    }
}
