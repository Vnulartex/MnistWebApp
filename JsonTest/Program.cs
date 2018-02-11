using System;
using Newtonsoft.Json;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using Accord.Math;

namespace JsonTest
{
    class Program
    {
        static void Main(string[] args)
        {
           // string netData = new StreamReader("ver1.json").ReadToEnd();
           //// Net net = JsonConvert.DeserializeObject<Net>(netData);
           // double[][] activations = new double[net.sizes.Length][];
           // for (int i = 0; i < activations.Length; i++)
           // {
           //     activations[i]=new double[net.sizes[i]];
           // }
           // string val=new StreamReader("7.json").ReadToEnd();
           // Input input = JsonConvert.DeserializeObject<Input>(val);
           // activations[0] = input.values;
           // net.FeedForward(activations);
            
           // Console.ReadKey();
        }


        class Input
        {
            public double[] values;
            public double[] labels;
        }
       

        
    }
}
