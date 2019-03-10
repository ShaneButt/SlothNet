using System;
using System.Collections.Generic;
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

            }
        }

        public void ConnectLayers(NeuralLayer from, NeuralLayer to)
        {
            foreach(Neuron neuron in to.Neurons)
            {
                foreach(Neuron n in from.Neurons)
                {
                    neuron.Dendrites.Add(new Dendrite()
                    {
                        Input = neuron.Output,
                        Weight = to.Weight
                    });
                }
            }
        }

        public void Train()
        {
            // Perceptron Learning Algorithm
        }
    }
}
