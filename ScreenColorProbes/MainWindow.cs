using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace ScreenColorGrabber
{
    public partial class MainWindow : Form
    {
        int areaDepth = 100;
        int areaCount = 158;
        int intervalMS = 50;

        List<Area> areas = new List<Area>();
        List<Label> labels = new List<Label>();

        Font labelFont = new Font("Arial", 7);

        // Get screen's dimensions
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public MainWindow()
        {
            InitializeComponent();

            // Size window to take half of the screen and center it
            ClientSize = new Size(screenWidth / 2, screenHeight / 2);
            BackColor = Color.Black;
            CenterToScreen();

            // Set timer interval
            refreshTimer.Interval = intervalMS;

            // Set comboBox with list of screens
            screenList.Left = ClientSize.Width / 2 - screenList.Width / 2;
            screenList.Top = ClientSize.Height / 2 - screenList.Height / 2;
            screenList.Items.Clear();

            var dd = new DISPLAY_DEVICE();
            // var screens = EnumDisplayDevices(null, dd.DeviceName);

            foreach (var screen in Screen.AllScreens)
            {
                screenList.Items.Add(new ScreenItem(screen.DeviceName, screen.Bounds.Width, screen.Bounds.Height));
            }
            screenList.SelectedIndex = 0;

            // Make window as static as possible to ease positioning of controls
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            // Create a bitmap to be used as canvas for image manipulation
            Bitmap bitmap = new Bitmap(screenWidth, screenHeight);

            // Create areas and place them automatically
            // TODO: Look for existing config and load areas from there

            var probes = autoArrangeAreas(bitmap.Width, bitmap.Height, areaCount);

            float scaleX = 0.0f;
            float scaleY = 0.0f;

            horizontalScale.ValueChanged += (a, e) => {
                scaleX = horizontalScale.Value / 100.0f;
                refreshProbes(probes, scaleX, scaleY);
            };

            verticalScale.ValueChanged += (a, e) => {
                scaleY = verticalScale.Value / 100.0f;
                refreshProbes(probes, scaleX, scaleY);
            };

            refreshProbes(probes, scaleX, scaleY);

            // Add probing to timer
            refreshTimer.Tick += (sender, e) =>
            {
                using (var g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

                    var averageColors = ImageUtil.AverageColor(bitmap, areas.ToArray());

                    for (int i = 0; i < averageColors.Length; i++)
                    {
                        Color color = averageColors[i];
                        Label label = labels[i];

                        byte[] rgb = { color.R, color.G, color.B };
                        string hexColor = BitConverter.ToString(rgb).Replace("-", "");

                        label.BackColor = color;
                        // label.Text = i + ": " + (label.Left * 2 + label.Width * 2) + "x" + (label.Top * 2);
                        // label.Text = "R " + color.R.ToString() + "\nG " + color.G.ToString() + "\nB " + color.B.ToString();
                        // label.Text = "#" + hexColor;
                        label.Text = "";
                        label.ForeColor = (color.R + color.G + color.B) / 3 < 120 ? Color.White : Color.Black;

                        // TODO: Add code to control LED stripe
                        // TODO: Source: https://github.com/arvydas/BlinkStickDotNet
                    }
                }
            };
        }

        private void refreshProbes(ProbePoints probes, float scaleX, float scaleY)
        {
            refreshTimer.Stop();

            foreach (var label in labels)
            {
                Controls.Remove(label);
            }

            areas.Clear();
            labels.Clear();

            // Add top probes
            foreach (int probeX in probes.X)
            {
                addProbe(
                    probeX * (1 - scaleX * 2) + (screenWidth * scaleX), // X
                    0 + (screenHeight * scaleY), // Y
                    probes.Width * (1 - scaleX * 2), // W
                    areaDepth * (1 - scaleY) // H
                );
            }

            // Reverse direction for bottom probes
            probes.X.Reverse();

            // Add right probes
            foreach (int probeY in probes.Y)
            {
                addProbe(
                    screenWidth - areaDepth - (screenWidth * scaleX), // X
                    probeY * (1 - scaleY * 2) + (screenHeight * scaleY), // Y
                    areaDepth * (1 - scaleX), // W
                    probes.Height * (1 - scaleY * 2) // H
                );
            }

            // Reverse direction for left probes
            probes.Y.Reverse();

            // Add bottom probes
            foreach (int probeX in probes.X)
            {
                addProbe(
                    probeX * (1 - scaleX * 2) + (screenWidth * scaleX), // X
                    (screenHeight - areaDepth) - (screenHeight * scaleY), // Y
                    probes.Width * (1 - scaleX * 2), // W
                    areaDepth * (1 - scaleY) // H
                );
            }

            // Add left probes
            foreach (int probeY in probes.Y)
            {
                addProbe(
                    0 + (screenWidth * scaleX), // X
                    probeY * (1 - scaleY * 2) + (screenHeight * scaleY), // Y
                    areaDepth * (1 - scaleX), // W
                    probes.Height * (1 - scaleY * 2) // H
                );
            }

            refreshTimer.Start();
        }

        private void addProbe(float x, float y, float width, float height)
        {
            Label label = new Label
            {
                Width = (int)Math.Ceiling(width / 2),
                Height = (int)Math.Ceiling(height / 2),
                Left = (int)Math.Ceiling(x / 2),
                Top = (int)Math.Ceiling(y / 2),

                // BackColor = Color.HotPink,
                // Text = x + "x" + y,
                // Font = labelFont,
                // TextAlign = ContentAlignment.MiddleCenter,
                BorderStyle = BorderStyle.None,
                Visible = true,
            };

            Controls.Add(label);

            areas.Add(new Area((int)x, (int)y, (int)(width * 0.1), (int)(height * 0.1)));
            labels.Add(label);
        }

        /**
         * Takes the screen size and number of probes to distribute around the screen.
         * Returns the offsets of the probes in X and Y direction to 0,0 of the screen.
         * The returned object only contains half of the points. X for top and bottom and Y for left and right.
         * It also contains the necessary offset to the respective edges of the screen.
         * 
         * param name="screenWidth" 
         * param name="screenHeight"
         * param name="probeCount" Number of screen probes (must be an even number)
         * 
         * returns ProbePoints or null if probe count is odd
         */
        private ProbePoints autoArrangeAreas(int screenWidth, int screenHeight, int probeCount)
        {
            if (probeCount % 2 != 0)
            {
                return null;
            }

            float ratio = screenWidth / screenHeight;

            int halfCount = probeCount / 2;

            int countVertical = (int)Math.Floor(halfCount / ratio);
            int countHorizontal = halfCount - countVertical;

            int cellWidth = (int)Math.Floor((double)screenWidth / (double)countHorizontal);
            int cellHeight = (int)Math.Floor((double)screenHeight / (double)countVertical);

            int offsetX = (int)Math.Floor((double)cellWidth / 2);
            int offsetY = (int)Math.Floor((double)cellHeight / 2);

            List<int> pointsX = new List<int>();
            for (int i = 0; i < countHorizontal; i++)
            {
                pointsX.Add(cellWidth  * i);
            }

            List<int> pointsY = new List<int>();
            for (int i = 0; i < countVertical; i++)
            {
                pointsY.Add(cellHeight * i);
            }

            ProbePoints probePoints = new ProbePoints(
                X: pointsX,
                Y: pointsY,
                Width: cellWidth,
                Height: cellHeight
            );

            return probePoints;
        }
    }
}
