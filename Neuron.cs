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
        public double Error;

        public Neuron()
        {
            Dendrites = new List<Dendrite>();
            Output = new Pulse();
        }

        public void Fire()
        {
            //Console.WriteLine("PRE-SUM: " + Output.Value);
            Output.Value = Sum();
            //Console.WriteLine("POST-SUM: " + Output.Value);
            Output.Value = Activation(Output.Value);
            //Console.WriteLine("POST-ACT: " + Output.Value);
        }

        public void UpdateWeight(double newWeight)
        {
            foreach(Dendrite d in Dendrites)
            {
                d.Weight = newWeight;
            }
        }

        public void AdjustWeight(double err)
        {
            Error = err;
            foreach(Dendrite d in Dendrites)
            {
                d.Weight += Error * Activation(d.Weight) * 0.2 * Output.Value;
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
            double val = 1 / (1 + Math.Exp(-input));
            return (val * (1 - val) >= 0.00000000001) ? 1 : 0;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
