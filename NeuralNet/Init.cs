using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;


namespace NeuralNet
{
   public static  class Init
    {
        public static Image InitBitmap(string baseString)
        {
            byte[] data = Convert.FromBase64String(baseString);
            MemoryStream ms = new MemoryStream(data);
            return Image.FromStream(ms);
        }

        public static Net InitNetwork (string data)
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
    }
}