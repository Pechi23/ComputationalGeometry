using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputationalGeometry
{
    internal static class Engine
    {
        static int n = 10;
        static Bitmap bmp;
        static Graphics grp;
        public static void MinimumAreaRectangle(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);

            int xmin = points[0].X;
            int ymin = points[0].Y;
            int xmax = points[0].X;
            int ymax = points[0].Y;

            for (int i = 1;i< points.Length;i++) 
            {
                if (points[i].X < xmin)
                    xmin = points[i].X;
                if (points[i].Y < ymin)
                    ymin = points[i].Y;
                if (points[i].X > xmax)
                    xmax = points[i].X;
                if (points[i].Y > ymax)
                    ymax = points[i].Y;
            }
            MyPoint a = new MyPoint(xmin, ymin);
            MyPoint b = new MyPoint(xmax, ymax);


            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawRectangle(a,b, grp);
            pictureBox.Image = bmp;
        }
        public static void TwoSetsUnion(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points1 = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint[] points2 = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            List<Segment> segments = new List<Segment>();

            for (int i = 0;i < points1.Length;i++) 
            {
                double distMin = double.MaxValue;
                int to = 0; 
                for (int j = 1; j < points2.Length; j++)
                {
                    if (points1[i].Distance(points2[j])<distMin)
                    {
                        to = j;
                        distMin = points1[i].Distance(points2[j]);
                    }
                }
                segments.Add(new Segment(points1[i], points2[to]));
            }

            GraphicsHelper.DrawPoints(points1, grp, new Pen(Color.Black, 3), 3);
            GraphicsHelper.DrawPoints(points2, grp, new Pen(Color.Red, 3), 3);
            GraphicsHelper.DrawSegments(segments, grp);
            pictureBox.Image = bmp;
        }
        public static void FindLargestCircle(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);
            
            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint q = RandomGenerator.GenerateRandomPoint(0, pictureBox.Width, 0, pictureBox.Height);
            Circle circle = new Circle(q,0);

            double distMin = double.MaxValue;
            for (int i = 0; i < points.Length; i++)
            {
                if (q.Distance(points[i]) < distMin)
                {
                    distMin = q.Distance(points[i]);
                    circle.Radius = distMin;
                }
            }

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawPoint(q, grp,new Pen(Color.Red,3),3);
            GraphicsHelper.DrawCircle(circle, grp);
            
            pictureBox.Image = bmp;
        }
        public static void FindPointsCloserThanD(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint q = RandomGenerator.GenerateRandomPoint(0, pictureBox.Width, 0, pictureBox.Height);
            const int D = 400;
            List<Segment> segments = new List<Segment>();

            double distMin = double.MaxValue;
            for (int i = 0; i < points.Length; i++)
                if (q.Distance(points[i]) < D)
                    segments.Add(new Segment(q, points[i]));

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawPoint(q, grp, new Pen(Color.Red, 3), 3);
            GraphicsHelper.DrawSegments(segments, grp);
            pictureBox.Image = bmp;
        }
        public static void ConvexHullJarvis(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            List<Segment> convexHull = new List<Segment>();

            Array.Sort(points, (a, b) => a.X.CompareTo(b.X));
            int p = 0;
            do
            {
                
                int q = (p + 1) % n;

                for (int i = 0; i < n; i++)
                    if (MyMath.Determinant(points[p], points[q], points[i]) < 0)
                        q = i;

                convexHull.Add(new Segment(points[p], points[q]));
                p = q;

            } while (p != 0);

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawSegments(convexHull, grp);
            pictureBox.Image = bmp;
        }
        public static void ConvexHullSlow(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            List<Segment> convexHull = new List<Segment>();

            Array.Sort(points, (a, b) => a.Y.CompareTo(b.Y));
            bool ok = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    ok = true;
                    if (i != j)
                        for (int k = 0; k < n; k++)
                            if (MyMath.Determinant(points[i], points[j], points[k]) < 0 && i != k && j != k)
                                ok = false;
                    if (ok)
                        convexHull.Add(new Segment(points[i], points[j]));
                }
            }

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawSegments(convexHull, grp);
            pictureBox.Image = bmp;
        }
        public static void ShortestPairsBetweenPoints(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            
            List<Segment> pairs = new List<Segment>();
            bool[] visited = new bool[points.Length];

            Array.Sort(points, (a,b) => a.X.CompareTo(b.X));

            for (int i = 0; i < points.Length - 1; i++)
            {
                if (!visited[i])
                {
                    double distMin = double.MaxValue;
                    int to = i + 1;
                    for (int j = i + 1; j < points.Length; j++)
                    {
                        if (!visited[i] && !visited[j] && points[i].Distance(points[j]) < distMin)
                        {
                            to = j;
                            distMin = points[i].Distance(points[j]);
                        }
                    }
                    visited[i] = true;
                    visited[to] = true;
                    pairs.Add(new Segment(points[i], points[to]));
                }
            }

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawSegments(pairs, grp);
            pictureBox.Image = bmp;
        }
        public static void SweepLineSegmentsIntersection(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            List<Segment> segments = RandomGenerator.GenerateRandomSegments(n,pictureBox.Width,pictureBox.Height);
            
            Array.Sort(segments.ToArray(),(a,b) => a.From.CompareTo(b.From));

            Pen pen = new Pen(Color.Blue, 3);
            Pen pen2 = new Pen(Color.Red, 1);
            foreach (Segment segment in segments)
            {
                segment.DrawSegment(grp, new Pen(Color.Black,3));
                segment.DrawSweepLine(grp, pen2, bmp);
            }

            for (int i = 0; i < segments.Count - 1; i++)
                for (int j = i + 1; j < segments.Count; j++)
                    if (segments[i].Intersects(segments[j]))
                    {
                        segments[i].DrawSegment(grp, pen);
                        segments[j].DrawSegment(grp, pen);
                        segments[i].DrawintersectionSweepLine(grp, pen2,bmp, segments[j]);
                    }

            pictureBox.Image = bmp;
        }

    }
}
