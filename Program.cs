using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using ConsoleTableExt;

namespace SlothNet
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             * CSV files when put into a docx format
             * Will format into separate lines for separate rows
             * Each cell is separated by a comma
             * StreamReader .NextLine() and Split(',') to read into a DataTable object
             */
            string CSVFile = Environment.CurrentDirectory + @"/dataset.csv";
            DataTable[] Tables = CSVReader(CSVFile);
            DataTable Inputs = Tables[0];
            DataTable Outputs = Tables[1];


            NeuralNetwork Network = new NeuralNetwork();
            Network.AddLayer(new NeuralLayer(57, new Random().NextDouble(), "INPUT"));
            Network.AddLayer(new NeuralLayer(5, new Random().NextDouble(), "HIDDEN"));
            Network.AddLayer(new NeuralLayer(1, 0.1, "OUTPUT"));
            Network.Build();
            Console.WriteLine("------------BEFORE TRAINING------------");
            Network.DisplayNetwork();

            NeuralData InputData = new NeuralData(Inputs.Rows.Count);
            InputData.Add(Inputs);
            NeuralData OutputData = new NeuralData(Outputs.Rows.Count);
            OutputData.Add(Outputs);

            Network.Train(InputData, OutputData, 100);
            Console.WriteLine();
            Console.WriteLine("------------AFTER TRAINING------------");
            Network.DisplayNetwork();
            Console.ReadLine();
        }
        
        private static DataTable[] CSVReader(string CSV)
        {
            string[] Rows = File.ReadAllLines(CSV);
            string[] Cells = Rows[0].Split(',');

            DataTable Inputs = new DataTable();
            DataTable Outputs = new DataTable();

            int InputColumns = Cells.Length - 1;
            for (int i = 0; i < InputColumns; i++)
            {
                Inputs.Columns.Add($"Column {i}", typeof(double));
            }
            Outputs.Columns.Add("Output", typeof(double));

            // Fill Tables
            DataRow I_Row;
            DataRow O_Row;
            for(int i = 0; i < Rows.Length; i++)
            {
                Cells = Rows[i].Split(',');
                I_Row = Inputs.NewRow();
                O_Row = Outputs.NewRow();
                for(int x = 0; x < InputColumns; x++)
                {
                    I_Row[x] = Cells[x].ToString();
                }
                Inputs.Rows.Add(I_Row);
                O_Row[0] = Cells[InputColumns].ToString();
                Outputs.Rows.Add(O_Row);
            }

            return new DataTable[] { Inputs, Outputs };
        }

        private static void DisplayTable(DataTable tab)
        {
            ConsoleTableBuilder.From(tab).ExportAndWrite();
        }
    }
}
