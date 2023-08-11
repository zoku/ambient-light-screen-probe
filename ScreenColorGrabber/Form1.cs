using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace ScreenColorGrabber
{
    public partial class Form1 : Form
    {
        int areaDiameter = 100;
        int areaCount = 120;

        public Form1()
        {
            InitializeComponent();

            Width = Screen.PrimaryScreen.Bounds.Width / 2;
            Height = Screen.PrimaryScreen.Bounds.Height / 2;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;

            var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            int column = 0;
            int row = 0;

            List<Area> areas = new List<Area>();
            List<Label> labels = new List<Label>();

            for (int i=0; i < areaCount; i++)
            {
                Label label = new Label();

                if (i > 0 && i % 12 == 0)
                {
                    column = 0;
                    row++;
                }
                

                int offsetX = areaDiameter * column;
                int offsetY = areaDiameter * row;

                label.Width = areaDiameter / 2;
                label.Height = areaDiameter / 2;

                label.Left = (areaDiameter / 2) * column;
                label.Top = (areaDiameter / 2) * row;

                label.BackColor = Color.Pink;
                label.Text = "";
                label.BorderStyle = BorderStyle.FixedSingle;

                label.Visible = true;

                Controls.Add(label);

                areas.Add(new Area(offsetX, offsetY, areaDiameter, areaDiameter));
                labels.Add(label);

                column++;
            }

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

                        label.BackColor = color;
                        label.Text = "R " + color.R.ToString() + "\nG " + color.G.ToString() + "\nB " + color.B.ToString();
                    }
                }
            };
        }

        private ProbePoints autoArrangeAreas(int width, int height, int count)
        {
            if (count % 2 != 0)
            {
                return null;
            }

            float ratio = width / height;

            int halfCount = count / 2;

            int countHorizontal = (int)Math.Floor(halfCount / ratio);
            int countVertical = halfCount - countHorizontal;

            int cellWidth = (int)Math.Floor((double)width / (double)countHorizontal);
            int cellHeight = (int)Math.Floor((double)height / (double)countVertical);

            int offsetX = (int)Math.Floor((double)cellWidth / 2);
            int offsetY = (int)Math.Floor((double)cellHeight / 2);

            List<int> pointsX = new List<int>();
            for (int i = 0; i < countHorizontal; i++)
            {
                pointsX.Add(cellWidth  * i + offsetX);
            }

            List<int> pointsY = new List<int>();
            for (int i = 0; i < countVertical; i++)
            {
                pointsY.Add(cellHeight * i + offsetY);
            }

            ProbePoints probePoints = new ProbePoints(
                x: pointsX,
                y: pointsY,
                top: 0,
                right: width - cellWidth,
                bottom: height - cellHeight,
                left: 0
            );

            return probePoints;
        }

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
