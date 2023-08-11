using System.Collections.Generic;

namespace ScreenColorGrabber
{
    public partial class Form1
    {
        private class ProbePoints{
            List<int> x { get; set; }
            List<int> y { get; set; }

            int top { get; set; }
            int right { get; set; }
            int bottom { get; set; }
            int left { get; set; }

            public ProbePoints(List<int> x, List<int> y, int top, int right, int bottom, int left)
            {
                this.x = x;
                this.y = y;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
                this.left = left;
            }
        }
    }
}
