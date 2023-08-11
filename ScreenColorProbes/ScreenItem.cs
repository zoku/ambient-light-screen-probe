using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenColorGrabber
{
    internal class ScreenItem
    {
        string displayName { get; set; }
        int width { get; set; }
        int height { get; set; }

        public ScreenItem(string displayName, int width, int height)
        {
            this.displayName = displayName;
            this.width = width;
            this.height = height;
        }

        public override string ToString()
        {
            return displayName;
        }
    }
}
