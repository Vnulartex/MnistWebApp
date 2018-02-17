using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NeuralNet
{
    public static class ExtensionBitmap
    {
        public static Bitmap Resize(this Bitmap origBitmap,int width, int height)
        {
            Bitmap newBitmap = new Bitmap(width, height);
            ImageAttributes imageAttributes=new ImageAttributes();
            imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
            using (Graphics gr = Graphics.FromImage(newBitmap))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.DrawImage(origBitmap, new Rectangle(0,0,newBitmap.Width,newBitmap.Height),0,0,origBitmap.Width,origBitmap.Height,GraphicsUnit.Pixel,imageAttributes);
            }
            return newBitmap; 
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
            return 1-(float)pixel.R/255;
        }
    }
}
