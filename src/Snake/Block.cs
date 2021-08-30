using System.Drawing;

namespace Snake
{
    internal class Block
    {
        private Color color;
        private int size;
        public Point point;

        public Block()
        {
        }

        public Block(Color color, int size, Point p)
        {
            this.color = color;
            this.size = size;
            this.point = p;
        }

        public virtual void Paint(Graphics g)
        {
            SolidBrush sb = new SolidBrush(color);
            lock (g)
            {
                try
                {
                    g.FillRectangle(sb,
                        //point.X * this.size,
                        //point.Y * this.size,
                        point.X * this.size,
                        point.Y * this.size,
                        this.size - 1,
                        this.size - 1);
                }
                catch { }
            }
        }
    }
}