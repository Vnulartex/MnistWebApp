using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace NeuralNet
{
    public static class ExtensionBitmap
    {
        public static Bitmap Resize(this Bitmap origBitmap, int newWidth, int newHeight,int newX=0,int newY=0,int sourceX=0,int sourceY=0,int sourceWidth=0,int sourceHeight=0)
        {
            if (sourceWidth == 0)
                sourceWidth = origBitmap.Width;
            if (sourceHeight == 0)
                sourceHeight = origBitmap.Height;
            Bitmap newBitmap = new Bitmap(newWidth, newHeight);
            ImageAttributes imageAttributes=new ImageAttributes();
            imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
            using (Graphics gr = Graphics.FromImage(newBitmap))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.DrawImage(origBitmap, new Rectangle(0,0,newBitmap.Width,newBitmap.Height),sourceX,sourceY,sourceWidth,sourceHeight,GraphicsUnit.Pixel,imageAttributes);
            }
            return newBitmap; 
        }

        public static double[][] ToArray(this Bitmap imageBitmap, int arraySize)
        {
            double[][] pixelDoubles=new double[arraySize][];
            for (int i = 0; i < arraySize; i++)
            {
                pixelDoubles[i]=new double[arraySize];
                for (int j = 0; j < arraySize; j++)
                {
                    pixelDoubles[i][j] = imageBitmap.GetGrayscale(j, i);
                }
            }
            return pixelDoubles;
        }

        public static double GetGrayscale(this Bitmap bmp,int x,int y)
        {
            Color pixel = bmp.GetPixel(x, y);
            return 1-(float)pixel.R/255;
        }

        private static Rectangle FindBounds(this Bitmap bmp,float threshold)
        {
            Rectangle bounds = new Rectangle();
            //Left
            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (bmp.GetGrayscale(x, y) > threshold)
                        bounds.X = x;
                }
            }
            //Right
            for (int x = bmp.Width-1; x > 0; x--)
            {
                for (int y = bmp.Height-1; y > 0; y--)
                {
                    if (bmp.GetGrayscale(x, y) > threshold)
                        bounds.Width = x-bounds.X;
                }
            }
            //Top
            for (int y = bmp.Height-1; y > 0; y--)
            {
                for (int x = bmp.Width-1; x > 0; x--)
                {
                    if (bmp.GetGrayscale(x, y) > threshold)
                        bounds.Y = y;
                }
            }
            //Bottom
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    if (bmp.GetGrayscale(x, y) > threshold)
                        bounds.Height = bounds.Y-y ;
                }
            }
            return bounds;
        }

        public static Bitmap Center(this Bitmap origBmp)
        {

            //Point leftTop = Loop(origBmp, origBmp.Height, origBmp.Width, 1);
            //Point rightBottom = Loop(origBmp, 0, 0, -1);
            //Rectangle rect = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
            //using (Graphics g = Graphics.FromImage(origBmp))
            //{
            //    g.Clear(Color.White);
            //    g.DrawImage();
            //}
            return origBmp;
        }

        public static Bitmap Normalize(this Bitmap bmp)
        {
            Rectangle bounds = bmp.FindBounds(0);
            float factor = (float)bounds.Width / bounds.Height;
            int newWidth = 20;
            int newHeight = 20;
            if (factor > 1)
                newHeight = (int)(newWidth / factor);
            else if (factor < 1)
                newWidth = (int) (newHeight / factor);
            int newX = (20 - newWidth) / 2;
            int newY = (20 - newHeight) / 2;
            bmp = bmp.Resize(newWidth, newHeight,newX,newY, bounds.X, bounds.Y, bounds.Width, bounds.Height);//vycentrovat ve 20x20 ramecku
            newX = (28 - 20) / 2;
            newY = (28 - 20) / 2;    
            return bmp.Resize(28, 28, newX, newY);

        }
    }
}
