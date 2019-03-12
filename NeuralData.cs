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
        private int Rows;
        private int Row = 0;

        public NeuralData(int rows)
        {
            Rows = rows;
            Data = new double[rows][];
        }

        public void Add(DataTable dataTable)
        {
            double[] data = new double[Rows];
            foreach(DataRow row in dataTable.Rows)
            {

            }
        }
    }
}
