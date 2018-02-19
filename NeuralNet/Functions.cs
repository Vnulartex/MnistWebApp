using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace NeuralNet
{
   public static  class Functions
    {
        public static Bitmap Center(Bitmap origBmp)
        {

            Point leftTop = Loop(origBmp, origBmp.Height, origBmp.Width, 1);
            Point rightBottom = Loop(origBmp, 0, 0, -1);
            Rectangle rect = new Rectangle(leftTop.X, leftTop.Y, rightBottom.X - leftTop.X, rightBottom.Y - leftTop.Y);
            using (Graphics g = Graphics.FromImage(origBmp))
            {
                g.Clear(Color.White);
               // g.DrawImage();
            }
            return origBmp;
        }

        public static Image CreateBitmap(string baseString)
        {
            byte[] data = Convert.FromBase64String(baseString);
            MemoryStream ms = new MemoryStream(data);
            return Image.FromStream(ms);
        }

        private static Point Loop(Bitmap origBmp,int heightBorder,int widthBorder,int increment)
        {
            Point leftTop = new Point();
            int x = 0;
            int y = 0;
            if (increment == -1)
            { 
                x = origBmp.Width;
                y = origBmp.Height;
            }
            for (x = 0; x < widthBorder; x+=increment)
            {
                for (y = 0; y < heightBorder; y+=increment)
                {
                    if (origBmp.IsBlack(x, y)>0.1)
                    {
                        leftTop.X = x;
                        leftTop.Y = y;
                        return leftTop;
                    }
                }
            }
            return leftTop;
        }
    }
}