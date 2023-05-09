using ComputationalGeometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class Circle
    {
        public MyPoint Center { get; set; }
        public double Radius { get; set; }
        public Circle() { }

        public Circle(MyPoint center, double radius) 
        { 
            Center = center;
            Radius = radius;
        }

        public bool IsInside(MyPoint point)
        {
            return Center.Distance(point) <= Radius;
        }

        public bool AllAreInside(MyPoint[] points)
        {
            foreach(MyPoint point in points)
                if(!IsInside(point))
                    return false;
            return true;
        }

        public void DrawCircle(Graphics grp,Pen pen)
        {
            GraphicsHelper.DrawCircle(this ,grp, pen);
        }

        public void DrawCircle(Graphics grp)
        {
            GraphicsHelper.DrawCircle(this, grp);
        }
    }
}
