using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class NeuralLayer
    {
        public List<Neuron> Neurons { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }

        public NeuralLayer(int count, double initWeight, string name = "NetworkLayer")
        {
            Neurons = new List<Neuron>();
            for(int i = 0; i < count; i++)
            {
                Neurons.Add(new Neuron());
            }
            Weight = initWeight;
            Name = name;
        }

        public void Compute(double error) // lr = learning rate
        {
            foreach (Neuron n in Neurons)
            {
                n.AdjustWeight(error);
            }
        }

        public void Forward()
        {
            foreach(Neuron n in Neurons)
            {
                n.Fire();
            }
        }

        public void Optimise(double lr, double error, double delta)
        {
            foreach(Neuron n in Neurons)
            {
                n.AdjustWeight(error);
            }
        }

        public void Display()
        {
            Console.WriteLine("Layer: {0}, Weight: {1}", Name, Weight);
        }
    }
}
