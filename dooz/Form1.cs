using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace dooz
{

    public partial class Form1 : Form
    {
        private bool turn = false;
        Node[] nodes = null;
        private int NodeCounter = 0;
        public Form1()
        {
            InitializeComponent();
            nodes = new Node[24];
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            NodeCounter = 0;

            Graphics g = e.Graphics;

            //playSimpleSound();

            DrawScreen(g, 40);
        }
        private void DrawScreen(Graphics g, int diameter)
        {
            //drawing rectangles of dooz
            g.FillRectangle(Brushes.Salmon, 100, 100, 600, 600);
            g.DrawRectangle(Pens.Black, 100, 100, 600, 600);
            g.FillRectangle(Brushes.CornflowerBlue, 150, 150, 500, 500);
            g.DrawRectangle(Pens.Black, 150, 150, 500, 500);
            g.FillRectangle(Brushes.Salmon, 200, 200, 400, 400);
            g.DrawRectangle(Pens.Black, 200, 200, 400, 400);


            //drawing circles of dooz
            DrawCircles(g, Brushes.Yellow, Pens.Black, 400, 100, 0, 50, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 400, 600, 0, 50, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 100, 100, 50, 50, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 700, 100, -50, 50, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 100, 400, 50, 0, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 700, 400, -50, 0, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 700, 700, -50, -50, diameter);
            DrawCircles(g, Brushes.Yellow, Pens.Black, 100, 700, 50, -50, diameter);

            //drawing turn rectangles
            if (turn == false)
                g.FillRectangle(Brushes.Red, 350, 350, 100, 100);
            else
                g.FillRectangle(Brushes.Green, 350, 350, 100, 100);
            g.DrawRectangle(Pens.Black, 350, 350, 100, 100);
        }

        /// </summary>
        /// <param name="g"></param>
        /// <param name="p"></param>
        /// <param name="startX"></entered x>
        /// <param name="startY"></entered y>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="diameter"></shoa>
        private void DrawCircles(Graphics g, Brush b, Pen p, int startX, int startY, int offsetX, int offsetY, int diameter)
        {

            int x = startX - diameter / 2;
            int y = startY - diameter / 2;
            for (int i = 0; i < 3; i++)
            {
                g.FillEllipse(Brushes.Yellow, x, y, diameter, diameter);
                g.DrawEllipse(Pens.Black, x, y, diameter, diameter);

                if (nodes[NodeCounter] == null)
                {
                    nodes[NodeCounter] = new Node { Number = NodeCounter + 1, X = x, Y = y, State = 0 };
                }

                if (nodes[NodeCounter].State == 0)
                    g.FillEllipse(b, x, y, diameter, diameter);
                else if (nodes[NodeCounter].State == 1)
                    g.FillEllipse(Brushes.Red, x, y, diameter, diameter);
                else
                    g.FillEllipse(Brushes.Green, x, y, diameter, diameter);



                g.DrawEllipse(Pens.Black, x, y, diameter, diameter);

                string str = (NodeCounter + 1).ToString();

                var sizeString = g.MeasureString(str, SystemFonts.DefaultFont);

                g.DrawString((NodeCounter + 1).ToString(), SystemFonts.DefaultFont, Brushes.Black, x + diameter / 2 - sizeString.Width / 2, y + diameter / 2 - sizeString.Height / 2);
                NodeCounter++;
                x += offsetX;
                y += offsetY;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Width = 800;
            this.Height = 800;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (nodes[(int)numericUpDown1.Value - 1].State == 0)
            {
                if (turn == false)
                    nodes[(int)numericUpDown1.Value - 1].State = 1;
                else
                    nodes[(int)numericUpDown1.Value - 1].State = 2;

                this.Invalidate();

                int chk = Check();
                if (chk != 0)
                {
                    MessageBox.Show((chk == 1) ? "Red is won" : "Green is won");
                    nodes = new Node[24];
                }

                turn = !turn;
                this.Invalidate();
            }
        }

        private int Check()
        {
            for (int i = 0; i < 24; i += 3)
            {
                if (nodes[i].State != 0 && nodes[i].State == nodes[i + 1].State && nodes[i + 1].State == nodes[i + 2].State)
                    return nodes[i].State;
            }

            int[,] arr = new int[12, 3]
                {
                    {1,7,10},
                    {2,8,11},
                    {3,9,12},
                    {4,21,24},
                    {5,20,23},
                    {6,19,22},
                    {7,13,19},
                    {8,14,20},
                    {9,15,21},
                    {10,16,22},
                    {11,17,23},
                    {12,18,24}
                };

            for (int i = 0; i < 12; i++)
            {
                if (Check3(arr[i, 0], arr[i, 1], arr[i, 2]))
                    return nodes[arr[i, 0]].State;

            }

            return 0;
        }

        private bool Check3(int i, int j, int k)
        {
            if (nodes[i].State == nodes[j].State && nodes[j].State == nodes[k].State)
                return true;
            else
                return false;
        }
        /*
        private void playSimpleSound()
        {
            SoundPlayer s = new SoundPlayer(@"C: \Users\Mohammad\Desktop\pirates_of_the_caribbean_theme_song");
            s.Play();
        }
        */
    }
}
