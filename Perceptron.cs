using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothNet
{
        /// <summary>
    /// An object oriented perceptron.
    /// <remarks>
    /// Refactored from a procedural version at:
    /// <seealso cref="http://www.codingvision.net/miscellaneous/c-perceptron-tutorial"/>
    /// </remarks>
    /// </summary>
    public class Perceptron
    {
        private static readonly Random _random = new Random();

        private readonly double[] _weights;
        private readonly double[] _inputs;

        private double _biasWeight;
        private const double _bias = 1;

        private const double learningRate = 1.0d;

        public double TotalError { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Perceptron"/> class.
        /// </summary>
        /// <param name="size">The size</param>
        public Perceptron(int size)
        {
            _weights = new double[size];
            _inputs = new double[size];

            _biasWeight = _random.NextDouble();

            for (var i = 0; i < size; i++)
            {
                _weights[i] = _random.NextDouble();
            }
        }

        /// <summary>
        /// Pulses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public double Pulse(double[] input)
        {
            #region set input(s)

            for (var i = 0; i < input.Length; i++)
            {
                _inputs[i] = input[i];
            }

            #endregion

            #region compute output

            var tmp = _inputs.Select((t, i) => ((IReadOnlyList<double>)_weights)[i] * t).Sum();

            tmp += _bias * _biasWeight;

            return tmp >= 0 ? 1 : 0;

            #endregion
        }

        /// <summary>
        /// Trains the specified input against the desired output
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="desiredOutput">The desired output.</param>
        public void Train(double[] input, double desiredOutput)
        {
            var output = Pulse(input);

            var localError = desiredOutput - output;

            TotalError += Math.Abs(localError);

            for (var i = 0; i < input.Length; i++)
            {
                _weights[i] += learningRate * localError * _inputs[i];
            }

            _biasWeight += learningRate * localError * _bias;
        }

        /// <summary>
        /// Resets the error tracking.
        /// </summary>
        public void ResetErrorTracking()
        {
            TotalError = 0;
        }
    }
}
