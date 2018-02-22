using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;


namespace NeuralNet
{
    public static class ExtensionBitmap
    {
        public static Bitmap New(this Bitmap origBitmap, int canvasWidth, int canvasHeight, int newX = 0, int newY = 0,
            int sourceX = 0, int sourceY = 0, int sourceWidth = 0, int sourceHeight = 0, int newSourceWidth = 0,
            int newSourceHeight = 0)
        {
            if (sourceWidth == 0)
                sourceWidth = origBitmap.Width;
            if (sourceHeight == 0)
                sourceHeight = origBitmap.Height;
            if (newSourceWidth == 0)
                newSourceWidth = canvasWidth;
            if (newSourceHeight == 0)
                newSourceHeight = canvasHeight;
            Bitmap newBitmap = new Bitmap(canvasWidth, canvasHeight);
            ImageAttributes imageAttributes=new ImageAttributes();
            imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
            using (Graphics gr = Graphics.FromImage(newBitmap))
            {         
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.Clear(Color.White);
                gr.DrawImage(origBitmap, new Rectangle(newX,newY,newSourceWidth,newSourceHeight),sourceX,sourceY,sourceWidth,sourceHeight,GraphicsUnit.Pixel,imageAttributes);
            }
            return newBitmap; 
        }

        public static double[][] ToArray(this Bitmap imageBitmap, int arraySize)
        {
            double[][] pixelDoubles=new double[imageBitmap.Height][];
            for (int i = 0; i < imageBitmap.Height; i++)
            {
                pixelDoubles[i]=new double[imageBitmap.Width];
                for (int j = 0; j < imageBitmap.Width; j++)
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

        private static Rectangle FindBounds(this Bitmap bmp)
        {
            int size = bmp.Width;
            Rectangle bounds = new Rectangle();
            bounds.X = bmp.FindBound(true, 0);
            bounds.Y = bmp.FindBound(false, 0);
            bounds.Width = bmp.FindBound(true, size-1)-bounds.X + 1;
            bounds.Height =  bmp.FindBound(false, size-1)-bounds.Y + 1;
            return bounds;
        }

        public static int FindBound(this Bitmap bmp,bool horizontal,int defVal)
        {
            int a = defVal;
            int b = defVal;
            int increment = 1;
            if (defVal!=0)
            {
                increment = -1;
            }

            for (a=defVal; a >= 0 && a < bmp.Width; a += increment)
            {
                for (b=defVal; b >= 0 && b < bmp.Width; b += increment)
                {
                    if (horizontal)
                    {
                        if (bmp.GetGrayscale(a, b) > 0)
                            return a;
                    }
                    else
                    {
                        if (bmp.GetGrayscale(b, a) > 0)
                            return a;
                    }
                }
            }
            throw new Exception();
        }

        public static Point CenterOfMass(this Bitmap bmp)
        {
            double sumX = 0;
            double sumY = 0;
            int count = 0;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    double val = bmp.GetGrayscale(j, i);
                    if (val > 0)
                    {
                        sumX += j * val;
                        sumY += i * val;
                        count++;
                    }
                }
            }
            int x = (int) sumX / count;
            int y = (int) sumY / count;
            return new Point(x,y);
        }

        public static Bitmap Normalize(this Bitmap bmp)
        {
            Rectangle bounds = bmp.FindBounds();
            int canvasWidth = 28;
            int canvasHeight = 28;
            float factor = (float) bounds.Width / bounds.Height;
            int newWidth = 20;
            int newHeight = 20;
            if (factor > 1)
                newHeight = (int) (newWidth / factor);
            else if (factor < 1)
                newWidth = (int) (newHeight * factor);
            int newX = (canvasWidth - newWidth) / 2;
            int newY = (canvasHeight - newHeight) / 2;
            bmp = bmp.New(canvasWidth, canvasHeight, newX, newY, bounds.X, bounds.Y, bounds.Width, bounds.Height,
                newWidth, newHeight); //scalovat v 20x20 ramecku a vložit do středu 28x28 ramecku
            Point center = bmp.CenterOfMass();
            int sourceX = newX;
            int sourceY = newY;
            newX = center.X - newWidth / 2;
            newY = center.Y - newHeight / 2;
            return bmp.New(canvasWidth, canvasHeight, newX, newY, sourceX, sourceY, newWidth, newHeight, newWidth,
                newHeight);

        }
    }
}
