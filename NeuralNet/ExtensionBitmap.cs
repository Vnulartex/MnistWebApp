using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet
{
    public static class ExtensionBitmap
    {
        public static Bitmap Resize(this Bitmap origBitmap,int width, int height)
        {
            return new Bitmap(origBitmap,width,height);
        }

        public static double[][] Enumerate(this Bitmap imageBitmap, int arraySize)
        {
            double[][] pixelDoubles=new double[arraySize][];
            for (int i = 0; i < arraySize; i++)
            {
                pixelDoubles[i]=new double[arraySize];
                for (int j = 0; j < arraySize; j++)
                {
                    pixelDoubles[i][j] = imageBitmap.IsBlack(j, i);
                }
            }
            return pixelDoubles;
        }

        public static double IsBlack(this Bitmap bmp,int x,int y)
        {
            Color pixel = bmp.GetPixel(x, y);
            return 1-(pixel.R + pixel.G + pixel.B)/(255*3);
        }
    }
}
