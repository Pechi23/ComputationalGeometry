using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    internal class Triangle
    {
        public MyPoint[] Points = new MyPoint[3];
        public Color[] Colors = new Color[3];

        public Triangle()
        {

        }

        public Triangle(MyPoint a, MyPoint b, MyPoint c) 
        {
            Points[0] = a;
            Points[1] = b;
            Points[2] = c;
            Colors[0] = Color.Black;
            Colors[1] = Color.Black;
            Colors[2] = Color.Black;
        }

        public bool ContainsColor(Color color)
        {
            for(int i=0;i<Colors.Length;i++)
                if (Colors[i] == color)
                    return true;
            return false;
        }

        public Color GetMissingColor()
        {
            if (!this.ContainsColor(Color.Red))
                return Color.Red;
            else
                if (!this.ContainsColor(Color.Green))
                    return Color.Green;
                else
                    return Color.Blue;
        }

        public int ContainsPoint(MyPoint point)
        {
            for(int i=0;i<Points.Length;i++)
                if (Points[i] == point)
                    return i;
            return -1;
        }

        public void DrawTriangleColors(Graphics grp)
        {
            GraphicsHelper.DrawPoint(this.Points[0], grp, new Pen(this.Colors[0], 5), 5);
            GraphicsHelper.DrawPoint(this.Points[1], grp, new Pen(this.Colors[1], 5), 5);
            GraphicsHelper.DrawPoint(this.Points[2], grp, new Pen(this.Colors[2], 5), 5);
        }


    }
}
