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
    class Net
    {
        public int[] Sizes { get; }
        public double[][][] Weights { get; }
        public double[][][] Biases { get; }

        public static Net Init (string data)
        {
            string netData = new StreamReader("ver1.json").ReadToEnd();
            Net net = JsonConvert.DeserializeObject<Net>(netData);
            //double[][] activations = new double[net.Sizes.Length][];
            //for (int i = 0; i < activations.Length; i++)
            //{
            //    activations[i]=new double[net.Sizes[i]];
            //}
            return net;
        }

        public double[][] FeedForward(double[][] a)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                for (int j = 0; j < Weights[i].Length; j++)
                {
                    a[i + 1][j] = Sigmoid(a[i].Dot(Weights[i][j]) + Biases[i][j][0]);
                }
            }
            return a;
        }

        private double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }
    }
}
