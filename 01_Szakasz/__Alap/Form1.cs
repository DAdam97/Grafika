using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace __Alap
{
    public partial class Form1 : Form
    {
        Graphics g;
        PointF p0, p1;

        int size = 5;

        int grabbed = -1;

        public Form1()
        {
            InitializeComponent();

            p0 = new PointF(100, 100);
            p1 = new PointF(600, 300);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

            MidPointFull(g, p0, p1, Color.Red);
            g.FillRectangle(Brushes.Black, p0.X, p0.Y, size, size);
            g.FillRectangle(Brushes.Black, p1.X, p1.Y, size, size);
        }

        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsGrabbed(p0, e.Location, size)) { grabbed = 0; }
            else if (IsGrabbed(p1, e.Location, size)) { grabbed = 1; }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (grabbed > -1)
            {
                switch (grabbed)
                {
                    case 0: p0.X = e.X; p0.Y = e.Y; break;
                    case 1: p1.X = e.X; p1.Y = e.Y; break;
                }

                Canvas.Refresh();
            }

        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            grabbed = -1;
        }

        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {

        }
        #endregion

        private void DDA(Graphics g, PointF p0, PointF p1, Color c)
        {
            float dx = (int)(p1.X - p0.X);
            float dy = (int)(p1.Y - p0.Y);
            float length = Math.Abs(dx);

            if (length < Math.Abs(dy))
                length = Math.Abs(dy);

            float nx = dx / length;
            float ny = dy / length;

            float x = p0.X;
            float y = p0.Y;

            Pixel((int)x, (int)y, c, g);

            for (int i = 0; i < length; i++)
            {
                x += nx;
                y += ny;
                Pixel((int)x, (int)y, c, g);
            }
        }

        private void MidPoint(Graphics g, PointF p0, PointF p1, Color c)
        {
            int dx = (int)(p1.X - p0.X);
            int dy = (int)(p1.Y - p0.Y);
            int d = 2 * dy - dx;
            int x = (int)p0.X;
            int y = (int)p0.Y;

            for (int i = 0; i < dx; i++)
            {
                Pixel(x, y, c, g);
                if (d > 0)
                {
                    y++;
                    d += 2 * (dy - dx);
                }
                else
                { d += 2 * dy; }

                x++;
            }

        }

        private void MidPointFull(Graphics g, PointF p0, PointF p1, Color c)
        {
            int dx = (int)Math.Abs(p1.X - p0.X); int sx = (int)Math.Sign(p1.X - p0.X);
            int dy = (int)Math.Abs(p1.Y - p0.Y); int sy = (int)Math.Sign(p1.Y - p0.Y);

            bool t;

            if (dx < dy)
            {
                int temp = dx;
                dx = dy;
                dy = temp;
                t = true;
            }
            else { t = false; }

            int d = 2 * dy - dx;
            int x = (int)p0.X; int y = (int)p0.Y;

            Pixel(x, y, c, g);

            while (x != (int)p1.X && y != (float)p1.Y)
            {
                if (d > 0)
                {
                    if (t) x += sx;
                    else y += sy;
                    d = d - 2 * dx;
                }

                if (t) y += sy;
                else x += sx;
                d = d + 2 * dy;

                Pixel(x, y, c, g);
            }
        }

        private void Pixel(int x, int y, Color c, Graphics g)
        {
            g.DrawRectangle(new Pen(c), x, y, .5f, .5f);
        }

        private bool IsGrabbed(PointF p, PointF mouseLocation, int size)
        {
            return p.X < mouseLocation.X && p.X + size > mouseLocation.X &&
                   p.Y < mouseLocation.Y && p.Y + size > mouseLocation.Y;
        }
    }
}

