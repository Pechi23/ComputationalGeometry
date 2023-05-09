using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class Circle
    {
        public MyPoint Center { get; set; }
        public double Radius { get; set; }
        public Circle() { }

        public Circle(MyPoint center, int radius) 
        { 
            Center = center;
            Radius = radius;
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
