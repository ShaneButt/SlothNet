using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
    class Neuron
    {
        public List<Dendrite> Dendrites { get; set; }
        public Pulse Output { get; set; }

        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            Output = new Pulse();
        }

        public void Fire()
        {
            Output.Value = Sum();
            Output.Value = Activation(Output.Value);
        }

        public void UpdateWeight(double newWeight)
        {
            foreach(Dendrite d in Dendrites)
            {
                d.Weight = newWeight;
            }
        }

        private double Sum()
        {
            double computed = 0.0f;
            foreach(Dendrite d in Dendrites)
            {
                computed += d.Input.Value * d.Weight;
            }
            return computed;
        }

        private double Activation(double input)
        {
            double thresh = 1;
            return input >= thresh ? 0 : thresh;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
