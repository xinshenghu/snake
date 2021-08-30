using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private Palette p;

        private void FormMain_Load(object sender, EventArgs e)
        {
            start();
        }

        private void start()
        {
            p = null;
            Thread.Sleep(1000);

            int width, height, size;
            width = height = 20;
            size = 15;

            this.pictureBox1.Width = width * size;
            this.pictureBox1.Height = height * size;
            this.Width = pictureBox1.Width + 40;
            this.Height = pictureBox1.Height + 60;

            p = new Palette(width, height, size, this.pictureBox1.BackColor, Graphics.FromHwnd(this.pictureBox1.Handle), 1);
            p.Start();
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.W || e.KeyCode == Keys.Up) && p.direction != Direction.Down)
            {
                p.direction = Direction.Up;
                return;
            }
            else if ((e.KeyCode == Keys.D || e.KeyCode == Keys.Right) && p.direction != Direction.Left)
            {
                p.direction = Direction.Right;
                return;
            }
            else if ((e.KeyCode == Keys.S || e.KeyCode == Keys.Down) && p.direction != Direction.Up)
            {
                p.direction = Direction.Down;
                return;
            }
            else if ((e.KeyCode == Keys.A || e.KeyCode == Keys.Left) && p.direction != Direction.Right)
            {
                p.direction = Direction.Left;
                return;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                start();
            }
            else if(e.KeyCode==Keys.Escape){
                this.Close();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (p != null)
            {
                p.PaintPalette(e.Graphics);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}