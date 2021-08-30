using System;
using System.Collections;
using System.Drawing;
using System.Timers;

namespace Snake
{
    internal class Palette
    {
        public int width = 20;
        public int height = 20;
        public Color bgcolor;
        public Graphics graghics;
        public ArrayList blocks;
        public Direction direction;
        public Timer timer;
        public Block food;
        public int size = 20;
        public int level = 1;
        public bool gameover = false;
        public int[] speed = new int[] { 500, 450, 400, 350, 300, 250, 200, 150, 100, 50 };

        public Palette(int width, int height, int size, Color color, Graphics g, int lvl)
        {
            this.width = width;
            this.height = height;
            this.bgcolor = color;
            this.size = size;
            this.level = lvl;
            this.blocks = new ArrayList();
            this.blocks.Insert(0, (new Block(Color.Red, this.size, new Point(width / 2, height / 2))));
            this.direction = Snake.Direction.Right;
            this.graghics = g;
        }

        //public Direction Direction() { return null; }

        public void Start()
        {
            this.food = GetFood();

            timer = new Timer(speed[this.level]);
            timer.Elapsed += new ElapsedEventHandler(OnBlockTimedEvent);
            timer.AutoReset = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Dispose();
        }

        public void OnBlockTimedEvent(object source, ElapsedEventArgs e)
        {
            this.Move();
            if (this.CheckDead())
            {
                this.timer.Stop();
                this.timer.Dispose();
                System.Windows.Forms.MessageBox.Show("Score: " + this.blocks.Count, "Game Over");
            }
        }

        public bool CheckDead()
        {
            Block head = (Block)(this.blocks[0]);
            if (head.point.X < 0 || head.point.Y < 0 || head.point.X > this.width || head.point.Y > this.height)
            {
                return true;
            }
            for (int i = 1; i < this.blocks.Count; i++)
            {
                Block b = (Block)this.blocks[i];
                if (head.point.X == b.point.X && head.point.Y == b.point.Y)
                {
                    this.gameover = true;
                    return true;
                }
            }

            this.gameover = false;
            return false;
        }

        public Block GetFood()
        {
            Block food = null;
            Random r = new Random();
            bool redo = false;
            while (true)
            {
                redo = false;
                int x = r.Next(this.width);
                int y = r.Next(this.height);
                for (int i = 0; i < this.blocks.Count; i++)
                {
                    Block b = (Block)(this.blocks[i]);
                    if (b.point.X == x && b.point.Y == y)
                    {
                        redo = true;
                    }
                }
                if (redo == false)
                {
                    food = new Block(Color.Black, this.size, new Point(x, y));
                    break;
                }
            }
            return food;
        }

        public void Move()
        {
            Point p = new Point();
            Block head = (Block)this.blocks[0];
            if (this.direction == Snake.Direction.Up)
            {
                p = new Point(head.point.X, head.point.Y - 1);
            }
            else if (this.direction == Snake.Direction.Down)
            {
                p = new Point(head.point.X, head.point.Y + 1);
            }
            else if (this.direction == Snake.Direction.Left)
            {
                p = new Point(head.point.X - 1, head.point.Y);
            }
            else if (this.direction == Snake.Direction.Right)
            {
                p = new Point(head.point.X + 1, head.point.Y);
            }

            Block b = new Block(Color.Red, this.size, p);
            if (this.food.point != p)
            {
                this.blocks.RemoveAt(this.blocks.Count - 1);
            }
            else
            {
                if (this.level < 9)
                {
                    this.level++;
                    timer.Interval = speed[level];
                }

                this.food = this.GetFood();
            }

            this.blocks.Insert(0, b);
            this.PaintPalette(this.graghics);
        }

        public void PaintPalette(Graphics gp)
        {
            gp.Clear(this.bgcolor);
            this.food.Paint(gp);
            foreach (Block b in this.blocks)
            {
                b.Paint(gp);
            }
        }
    }
}