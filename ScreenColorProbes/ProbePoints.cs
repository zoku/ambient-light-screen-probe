using System.Collections.Generic;

namespace ScreenColorGrabber
{
    public class ProbePoints{
        public List<int> X { get; set; }
        public List<int> Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public ProbePoints(List<int> X, List<int> Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }
    }
}
