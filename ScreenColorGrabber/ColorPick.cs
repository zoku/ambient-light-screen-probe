using System.Drawing;
using System.Runtime.InteropServices;
using System;

public class ColorPick {
    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindowDC(IntPtr window);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern uint GetPixel(IntPtr dc, int x, int y);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern uint BitBlt(IntPtr hdc, int x, int y, int w, int h, IntPtr hdcSrc, int x1, int y1, int rop);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int ReleaseDC(IntPtr window, IntPtr dc);

    // public static Color GetColorAt(int x, int y, int w, int h)
    // {
       //  IntPtr desk = GetDesktopWindow();
        // IntPtr dc = GetWindowDC(desk);

        // BitBlt(0, 0, 0, w, h, dc, x, y, 0);

        // ReleaseDC(desk, dc);

        // a /= (w * h);

        // int r = (a >> 0) & 0xff;
        // int g = (a >> 8) & 0xff;
        // int b = (a >> 16) & 0xff;

        // return Color.FromArgb(255, r / (w * h), g / (w * h), b / (w * h));
    // }

    public static void GetColorAt(int x, int y, int w, int h) {
        // Graphics.CopyFromScreen(x, y, 0, 0, new Size(w, h), CopyPixelOperation.Blackness);
    }
}