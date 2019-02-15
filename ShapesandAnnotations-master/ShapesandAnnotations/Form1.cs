using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace ShapesandAnnotations
{
    public partial class Form1 : Form
    {
        int x, y;
        static int count = 0;
        Shapes.AnnotationList annotations = new Shapes.AnnotationList();
        public Form1()
        {


            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

          //  pictureBox1.Image = new Bitmap(@"C:\Users\310296155\Pictures\Womens_day_2018\SRID1364.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.MouseClick += new MouseEventHandler(PictureBox1_MouseClick);
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            x = p.X;
            y = p.Y;
            int width = 100, height = 100;
            count = count + 1;


            if (comboBox1.SelectedIndex == 0)
            {
                annotations.Add(new Shapes.CircleShape()
                {
                    Rectangle = new Rectangle(x - width / 2, y - height / 2, width, height),
                    count = count,
                    x = x,
                    y = y

                }

                );
                annotations.Draw(pictureBox1.CreateGraphics());
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                annotations.Add(new Shapes.RectangleShape()
                {
                    Rectangle = new Rectangle(x, y, width, height / 2),
                    count = count,
                    x = x + 25,
                    y = y + 12

                });

                annotations.Draw(pictureBox1.CreateGraphics());
            }




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        private void btnSave_Click(object sender, EventArgs e)
        {

            annotations.Save(@"C:\Users\310296155\Downloads\ShapesandAnnotations-master\ShapesandAnnotations-master\ShapesandAnnotations\Images\shapes.bin");
            annotations.Clear();
            this.pictureBox1.Invalidate();
            MessageBox.Show("Shapes saved successfully.");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            annotations.Load(@"C:\Users\310296155\Downloads\ShapesandAnnotations-master\ShapesandAnnotations-master\ShapesandAnnotations\Images\shapes.bin");
            this.pictureBox1.Invalidate();


            annotations.Draw(pictureBox1.CreateGraphics());
            MessageBox.Show("Shapes loaded successfully.");
        }
    }
}
