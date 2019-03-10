using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class NeuralData
    {
        Dictionary<double[], double> Data { get; set; }
        int Row = 0;

        public NeuralData(int rows)
        {
            Data = new Dictionary<double[], double>(rows);
        }

        public void Add(double[] data, double desired)
        {
            Data.Add(data, desired);
            Row++;
        }
    }
}
