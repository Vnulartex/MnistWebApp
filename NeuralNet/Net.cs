using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;
using Newtonsoft.Json;

namespace NeuralNet
{
    public class Net
    {
        public int[] Sizes { get; set; }
        public double[][][] Weights { get; set; }
        public double[][][] Biases { get; set; }
        public double[][] Activations { get; set; }

        public static Net Init (string data)
        {
            string netData = new StreamReader(data).ReadToEnd();
            Net net = JsonConvert.DeserializeObject<Net>(netData);
            net.Activations = new double[net.Sizes.Length][];
            for (int i = 0; i < net.Activations.Length; i++)
            {
                net.Activations[i] = new double[net.Sizes[i]];
            }
            return net;
        }

        public double[] FeedForward(double[] a)
        {
            Activations[0] = a;
            for (int i = 0; i < Weights.Length; i++)
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    Activations[i + 1][j] = Sigmoid(Activations[i].Dot(Weights[i][j]) + Biases[i][j][0]);
                }
            }
            return Activations[Activations.Length-1];
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }
}
