using System.Runtime.InteropServices;

namespace ScreenColorGrabber
{
    internal static class DisplayUtil
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);
    }
}