using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

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
            DataTable Table = ReadCSV(CSVFile);
            Console.ReadLine();
        }

        private static DataTable ReadCSV(string CSVFilePath)
        {
            string[] Rows = File.ReadAllLines(CSVFilePath);
            string[] Cells = Rows[0].Split(',');
            DataTable Table = new DataTable();
            int Columns = Cells.Length;
            for (int i = 0; i < Columns; i++)
                Table.Columns.Add($"Column {i}", typeof(string));
            DataRow Row;
            for (int i = 0; i < Rows.Length; i++) // Run through every row
            {
                Cells = Rows[i].Split(',');
                Row = Table.NewRow();
                for (int x = 0; x < Columns; x++) // Run through every column
                {
                    //Console.WriteLine("Cell: " + Cells[x]);
                    Row[x] = Cells[x].ToString();
                }
                Table.Rows.Add(Row);
            }
            return Table;
        }

        private static void DisplayTable(DataTable tab)
        {
            foreach(DataRow row in tab.Rows)
            {
                Console.WriteLine();
                for(int i = 0; i < tab.Columns.Count; i++)
                {
                    Console.Write(row[i] + " | ");
                }
            }
        }
    }
}
