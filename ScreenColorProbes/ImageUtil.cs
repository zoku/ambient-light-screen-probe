using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace ScreenColorGrabber
{

    class Area
    {
        public Area(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    internal class ImageUtil
    {
        /**
         * Source: https://stackoverflow.com/questions/1068373/how-to-calculate-the-average-rgb-color-values-of-a-bitmap
         */
        public static Color[] AverageColor(Bitmap bmp, Area[] areas)
        {
            BitmapData srcData = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb
            );

            int stride = srcData.Stride;

            IntPtr Scan0 = srcData.Scan0;

            int width = bmp.Width;
            int height = bmp.Height;

            List<Color> colors = new List<Color>();

            foreach (Area area in areas)
            {
                if (area.x + area.w > width || area.y + area.h > height)
                {
                    colors.Add(Color.FromArgb(255, 255, 0, 255));
                    continue;
                }

                long[] totals = new long[] { 0, 0, 0 };

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;

                    for (int y = area.y; y < area.y + area.h; y++)
                    {
                        for (int x = area.x; x < area.x + area.w; x++)
                        {
                            for (int color = 0; color < 3; color++)
                            {
                                int idx = (y * stride) + x * 4 + color;

                                totals[color] += p[idx];
                            }
                        }
                    }
                }

                int avgB = (int)(totals[0] / (area.w * area.h));
                int avgG = (int)(totals[1] / (area.w * area.h));
                int avgR = (int)(totals[2] / (area.w * area.h));

                colors.Add(Color.FromArgb(255, avgR, avgG, avgB));
            }

            bmp.UnlockBits(srcData);

            return colors.ToArray();
        }
    }
}
