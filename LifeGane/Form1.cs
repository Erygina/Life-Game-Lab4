using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeGane
{
    public partial class Form1 : Form
    {
        bool[,] field;

        Bitmap bmp;
        Graphics graphics;
        Random rand = new Random();
        Pen pen = new Pen(Color.White, 1);
        int m, n, scale = 7; 

        public Form1()
        {
            InitializeComponent();
            m = pictureBox1.Height;
            n = pictureBox1.Width;
            field = new bool[m, n];

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bmp);
        }
        void create()
        {
            for (int i = 0; i < m; i++)    //по бокам неживые клетки
            {
                field[i, 0] = false;
                field[i, n - 1] = false;
            }
            for (int j = 0; j < n; j++)
            {
                field[0, j] = false;
                field[m - 1, j] = false;
            }

            for (int i = 1; i < m - 1; i++)    //случайно определяются на карте живые клетки
            {
                for (int j = 1; j < n - 1; j++)
                {
                    if (rand.Next(0, 4) >= 3)
                    {
                        field[i, j] = true;
                    }
                    else
                    {
                        field[i, j] = false;
                    }
                }
            }
        }
        private void render()
        {

            graphics = Graphics.FromImage(bmp);

            graphics.Clear(Color.Black);

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)   //прорисовка на карте
                {
                    if (field[i, j])
                    {
                        graphics.DrawRectangle(pen, i * scale, j * scale, scale, scale);
                    }
                }
            }

            pictureBox1.Image = bmp;

        }

        private int neighbours(int i, int j) 
        {
            int neigh = 0;
            if (field[i, j + 1])
            {
                neigh++;
            }
            if (field[i + 1, j + 1])
            {
                neigh++;
            }
            if (field[i + 1, j])
            {
                neigh++;
            }
            if (field[i + 1, j - 1])
            {
                neigh++;
            }
            if (field[i, j - 1])
            {
                neigh++;
            }
            if (field[i - 1, j - 1])
            {
                neigh++;
            }
            if (field[i - 1, j])
            {
                neigh++;
            }
            if (field[i - 1, j + 1])
            {
                neigh++;
            }
            return neigh;
        }
        private void step()
        {

            bool[,] next = new bool[m, n];
            for(int i = 1; i < m - 1; i++)
                for(int j = 1; j < n - 1; j++)
                {
                    int neigh = neighbours(i, j);
                    if (field[i, j])
                    {

                        if (neigh >= 2 && neigh <= 3)
                        {
                            next[i, j] = true;
                        }
                        else
                        {
                            next[i, j] = false;
                        }

                    }
                    else
                    {
                        if (neigh == 3)
                        {
                            next[i, j] = true;
                        }
                        else
                        {
                            next[i, j] = false;
                        }
                    }

                }
                field = next;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            create();
            render();
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            step();
            render();
        }
    }
}
