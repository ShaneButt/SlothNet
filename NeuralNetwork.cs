using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class NeuralNetwork // Perceptron learning based NN
    {
        public List<NeuralLayer> Layers { get; set; }

        public NeuralNetwork()
        {
            Layers = new List<NeuralLayer>();
        }

        public void AddLayer(NeuralLayer layer)
        {
            int dendrites = 1;
            if (Layers.Count > 0)
            {
                dendrites = Layers.Last().Neurons.Count();
            }
            foreach(Neuron n in layer.Neurons)
            {
                for(int i = 0; i < dendrites; i++)
                {
                    n.Dendrites.Add(new Dendrite());
                }
            }
            Layers.Add(layer);
        }

        public void Build()
        {
            int i = 0;
            foreach(NeuralLayer layer in Layers)
            {
                if(i>=Layers.Count - 1)
                {
                    break;
                }

                NeuralLayer next = Layers[i + 1];
                ConnectLayers(layer, next);
                i++;
            }
        }

        public void ConnectLayers(NeuralLayer from, NeuralLayer to)
        {
            foreach(Neuron n in from.Neurons)
            {
                n.Dendrites = new List<Dendrite>();
                n.Dendrites.Add(new Dendrite());
            }

            foreach(Neuron neuron in to.Neurons)
            {
                neuron.Dendrites = new List<Dendrite>();
                foreach(Neuron n in from.Neurons)
                {
                    neuron.Dendrites.Add(new Dendrite()
                    {
                        Input = n.Output,
                        Weight = to.Weight
                    });
                }
            }
        }

        public void Train(NeuralData Input, NeuralData Output, int iterations = 100, double lr = 0.1)
        {
            int epoch = 1;
            while(epoch <= iterations)
            {
                NeuralLayer inputLayer = Layers[0];
                List<double> outputs = new List<double>();

                for(int x = 0; x < Input.Data.Length; x++)
                {
                    for(int j = 0; j < Input.Data[x].Length; j++)
                    {
                        inputLayer.Neurons[j].Output.Value = Input.Data[x][j];
                    }
                    ComputeOutput();
                    outputs.Add(Layers.Last().Neurons.First().Output.Value);
                }

                double accuracy = 0;
                int count = 0;
                outputs.ForEach((x) =>
                {
                    if(x == Output.Data[count].First())
                    {
                        accuracy++;
                    }
                    count++;
                });

                OptimiseWeights(accuracy/count);
                Console.WriteLine("Epoch: {0}, Accuracy {1}%", epoch, accuracy/count);
                epoch++;
            }
        }

        public void ComputeOutput()
        {
            bool first = true;
            foreach(NeuralLayer layer in Layers)
            {
                if (first)
                    continue; first = false;
                layer.Forward();
            }
        }

        public void OptimiseWeights(double accuracy)
        {
            float lr = 0.1f;
            if (accuracy >= 0.95 && accuracy <= 1)
                return;

            if (accuracy > 1)
                lr = -lr;

            foreach(NeuralLayer Layer in Layers)
            {
                Layer.Optimise(lr, 1);
            }
        }

        public void DisplayNetwork()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Input");
            table.Columns.Add("Weight");
            table.Columns.Add("Hidden");
            table.Columns.Add("Weight");
            table.Columns.Add("Output");
            foreach (Neuron neuron in Layers[0].Neurons)
            {
                DataRow row = table.NewRow();
                row[0] = neuron;
                row[1] = Layers.First().Weight;
                table.Rows.Add(row);
            }
            foreach (Neuron neuron in Layers[1].Neurons)
            {
                DataRow row = table.NewRow();
                row[0] = neuron;
                row[1] = Layers.First().Weight;
                table.Rows.Add(row);
            }
            foreach (Neuron neuron in Layers[2].Neurons)
            {
                DataRow row = table.NewRow();
                row[4] = neuron.Output;
                table.Rows.Add(row);
            }
            ConsoleTableBuilder CTB = ConsoleTableBuilder.From(table);
            CTB.ExportAndWrite();
        }
    }
}
