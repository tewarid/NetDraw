﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Draw
{
    public partial class BaseControl : Control
    {
        bool moving = false;
        int leftOffset;
        int topOffset;

        public bool Selected { get; set; } = false;

        public BaseControl()
        {
            InitializeComponent();
            DoubleBuffered = true;
            this.MouseDown += Shape_MouseDown;
            this.MouseUp += Shape_MouseUp;
            this.MouseClick += Shape_MouseClick;
            this.MouseMove += Shape_MouseMove;
        }

        void Shape_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                Left = Left + e.X - leftOffset;
                Top = Top + e.Y - topOffset;
            }
        }

        void Shape_MouseClick(object sender, MouseEventArgs e)
        {
            Selected = true;
            Refresh();
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip.Show(PointToScreen(new Point(e.X, e.Y)));
            }
        }

        void Shape_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            Selected = true;
            leftOffset = e.X;
            topOffset = e.Y;
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle r = new Rectangle(3, 3, this.Width - 7, this.Height - 7);
            e.Graphics.DrawRectangle(Pens.Black, r);
            if (Selected)
            {
                Rectangle s = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                Pen p = new Pen(Color.Black);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                e.Graphics.DrawRectangle(p, s);                
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);
        }
    }
}
