using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shapes
{
    [Serializable]
    public abstract class Shape
    {
        public abstract void Draw(Graphics g);

        //   public Pen pen = new Pen(Color.Black);

        public int x
        { get; set; }
        public int y
        { get; set; }
    }
    [Serializable]
    public class RectangleShape : Shape
    {
        public RectangleShape()
        {
            color = Color.Blue;
        }
        public Rectangle Rectangle
        {
            get; set;
        }
        public Color color
        { get; set; }
        public int count
        { get; set; }
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(color);
            g.DrawRectangle(pen, Rectangle);
            g.FillRectangle(brush, Rectangle);
            g.DrawString(count.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.White, x, y);
        }
    }

    [Serializable]
    public class CircleShape : Shape
    {
        public CircleShape()
        {
            color = Color.Red;
        }

        public Rectangle Rectangle { get; set; }
        public Color color
        { get; set; }
        public int count
        { get; set; }
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            SolidBrush brush = new SolidBrush(color);
            g.DrawEllipse(pen, Rectangle);
            g.FillEllipse(brush, Rectangle);
            g.DrawString(count.ToString(), new Font("Arial", 12, FontStyle.Bold), Brushes.White, x, y);
        }
    }


    [Serializable]
    public class AnnotationList : List<Shape>
    {
        public void Save(string file)
        {
            using (Stream stream = File.Open(file, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, this);
            }
        }
        public void Load(string file)
        {
            using (Stream stream = File.Open(file, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                var shapes = (AnnotationList)bin.Deserialize(stream);
                this.Clear();
                this.AddRange(shapes);
                
            }
        }
        public void Draw(Graphics g)
        {
            this.ForEach(x => x.Draw(g));
        }
    }
}
