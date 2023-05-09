using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputationalGeometry
{
    public class GraphicsHelper
    {
        public static void DrawPoint(MyPoint point,Graphics grp,Pen pen,int size)
        {
            grp.DrawEllipse(pen, point.X, point.Y, size, size);
        }
        public static void DrawPoint(MyPoint point, Graphics grp)
        {
            DrawPoint(point, grp, new Pen(Color.Black, 3), 3);
        }
        public static void DrawPoints(MyPoint[] points, Graphics grp, Pen pen, int size)
        {
            foreach (MyPoint point in points)
                DrawPoint(point, grp, pen, size);
        }
        public static void DrawPoints(MyPoint[] points, Graphics grp)
        {
            DrawPoints(points, grp, new Pen(Color.Black, 3), 3);
        }
        public static void DrawPointsList(List<MyPoint> points, Graphics grp)
        {
            DrawPoints(points.ToArray(),grp);
        }
        public static void DrawPolygon(MyPoint[] points, Graphics grp)
        {
            for(int i=0;i<points.Length;i++)
            {
                DrawPoint(points[i],grp);
                grp.DrawLine(new Pen(Color.Blue, 3), points[i].X, points[i].Y, points[(i + 1) % points.Length].X, points[(i + 1) % points.Length].Y);
            }
        }
        public static void DrawSegment(Segment segment, Graphics grp)
        {
            grp.DrawLine(new Pen(Color.Blue, 3), segment.From.X, segment.From.Y, segment.To.X, segment.To.Y);
        }
        public static void DrawSegments(List<Segment> segments, Graphics grp)
        {
            for (int i = 0; i < segments.Count; i++)
                DrawSegment(segments[i],grp);
        }
        public static void DrawRectangle(MyPoint a,MyPoint b, Graphics grp)
        {
            Segment seg1 = new Segment(new MyPoint(a.X, a.Y), new MyPoint(b.X, a.Y));
            Segment seg2 = new Segment(new MyPoint(b.X, a.Y), new MyPoint(b.X, b.Y));
            Segment seg3 = new Segment(new MyPoint(b.X, b.Y), new MyPoint(a.X, b.Y));
            Segment seg4 = new Segment(new MyPoint(a.X, b.Y), new MyPoint(a.X, a.Y));
            List<Segment> segments = new List<Segment>() { seg1, seg2, seg3, seg4 };
            DrawSegments(segments, grp);
        }
        public static void DrawCircle(Circle circle,Graphics grp, Pen pen)
        {
            grp.DrawEllipse(pen, circle.Center.X - (int)circle.Radius, circle.Center.Y - (int)circle.Radius, (float)(circle.Radius * 2.0), (float)(circle.Radius * 2));
        }
        public static void DrawCircle(Circle circle, Graphics grp)
        {
            DrawCircle(circle, grp, new Pen(Color.Black, 3));
        }
    }
}
