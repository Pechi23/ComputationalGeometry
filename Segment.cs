using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class Segment
    {
        public MyPoint From { get; set; }
        public MyPoint To { get; set; }
        public double Length { get; private set; }
        public double LengthSquared { get; private set;}
        public Segment(MyPoint a,MyPoint b)
        {
            if(a.CompareTo(b)>0)
            {
                From = a;
                To = b;
            }
            else
            {
                From = b;
                To = a;
            }
            Length = a.Distance(b);
            LengthSquared = a.DistanceSquared(b);
        }
        public Segment()
        { }

        public void DrawSegment(Graphics g, Pen p)
        {
            GraphicsHelper.DrawSegment(this,g);
        }

        public bool Intersects(Segment other)
        {
            int o1 = Orientation(this.From, this.To, other.From);
            int o2 = Orientation(this.From, this.To, other.To);
            int o3 = Orientation(other.From, other.To, this.From);
            int o4 = Orientation(other.From, other.To, this.To);

            if (o1 != o2 && o3 != o4)
                return true;

            //TODO: treat special cases where segments overlap
            return false;
        }

        private int Orientation(MyPoint a, MyPoint b, MyPoint c)
        {
            int ret = a.X * b.Y + b.X * c.Y + c.X * a.Y - a.X * c.Y - b.X * a.Y - c.X * b.Y;
            if (ret == 0)
                return 0;
            else
                return ret > 0 ? 1 : -1;
        }

        public void DrawSweepLine(Graphics g, Pen p, Bitmap bmp)
        {
            Point Left = new Point(0, From.Y);
            Point Right = new Point(bmp.Width, From.Y);
            g.DrawLine(p, Left, Right);
            Point Left2 = new Point(0, To.Y);
            Point Right2 = new Point(bmp.Width, To.Y);
            g.DrawLine(p, Left2, Right2);
        }

        public void DrawintersectionSweepLine(Graphics g, Pen p, Bitmap bmp, Segment other)
        {
            int x1 = this.From.X;
            int y1 = this.From.Y;
            int x2 = this.To.X;
            int y2 = this.To.Y;
            int x3 = other.From.X;
            int y3 = other.From.Y;
            int x4 = other.To.X;
            int y4 = other.To.Y;

            int xcoord = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
            int ycoord = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

            Point Left = new Point(0, ycoord);
            Point Right = new Point(bmp.Width, ycoord);
            g.DrawLine(p, Left, Right);
        }
    }
}
