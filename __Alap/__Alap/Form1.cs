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

        public Form1()
        {
            InitializeComponent();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
        }

        #region Mouse Handling
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Canvas_MouseWheel(object sender, MouseEventArgs e)
        {

        }
        #endregion
    }
}
