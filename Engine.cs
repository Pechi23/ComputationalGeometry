﻿using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace ComputationalGeometry
{
    internal static class Engine
    {
        static Random rand = new Random();
        static int n = 10;
        static Bitmap bmp;
        static Graphics grp;
        public static bool DrawingEnabled = false;
        public static List<MyPoint> drawnPoints = new List<MyPoint>();
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

        public static void FindSmallestAreaTriangle(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);

            MyPoint[] triangle = new MyPoint[3];
            double minArea = double.MaxValue;
            for (int i = 0; i < points.Length - 2; i++)
                for (int j = i+1; j < points.Length - 1; j++)
                    for (int k = j+1; k < points.Length; k++)
                        if (0.5 * Math.Abs(MyMath.Determinant(points[i], points[j], points[k])) < minArea)
                        {
                            minArea = 0.5 * Math.Abs(MyMath.Determinant(points[i], points[j], points[k]));
                            triangle[0] = points[i];
                            triangle[1] = points[j];
                            triangle[2] = points[k];
                        }
            
            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawPolygon(triangle, grp);
            pictureBox.Image = bmp;
        }

        public static void FindSmallestAreaTriangleSorting(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            Array.Sort(points, (a, b) => (a.X * b.Y).CompareTo(b.X * a.Y));

            MyPoint[] triangle = new MyPoint[3];
            double minArea = double.MaxValue;
            for (int i = 0; i < points.Length - 2; i++)
            {
                if (0.5 * Math.Abs(MyMath.Determinant(points[i], points[i + 1], points[i + 2])) < minArea)
                {
                    minArea = 0.5 * Math.Abs(MyMath.Determinant(points[i], points[i + 1], points[i + 2]));
                    triangle[0] = points[i];
                    triangle[1] = points[i + 1];
                    triangle[2] = points[i + 2];
                }
            }
            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawPolygon(triangle, grp);
            pictureBox.Image = bmp;
        }

        public static void MinimumEnclosingCircle(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            Circle circle = new Circle();
            
            Array.Sort(points);
            FindMinimumEnclosingCircle(0, points.Length - 1,points,circle);
            if(!circle.AllAreInside(points))
                circle = FindMinimumEnclosingCircleSlow(points);

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawCircle(circle, grp);
            pictureBox.Image = bmp;
        }

        private static void FindMinimumEnclosingCircle(int left, int right,MyPoint[] points,Circle circle)
        {
            if (right <= left)
                return;

            if (right == left + 1)
            {
                circle.Center = MyMath.GetMidPoint(points[left], points[right]);
                circle.Radius = MyMath.Distance(points[left], points[right]) / 2;
                return;
            }

            // Find the smallest enclosing circle for the left and right subsets
            int mid = (left + right) / 2;
            FindMinimumEnclosingCircle(left, mid,points,circle);
            FindMinimumEnclosingCircle(mid + 1, right,points,circle);

            Circle circle1 = new Circle(circle.Center,circle.Radius);
            Circle circle2 = new Circle(MyMath.GetMidPoint(points[left], points[right]),MyMath.Distance(points[left], points[right]) / 2);
            // Merge the two circles

            if (circle1.IsInside(circle2.Center) && circle2.IsInside(circle1.Center))
            {
                circle.Center = MyMath.GetMidPoint(circle1.Center, circle2.Center);
                circle.Radius = (MyMath.Distance(circle1.Center, circle2.Center) + circle1.Radius + circle2.Radius) / 2;
            }
            else if (!circle1.IsInside(circle2.Center))
            {
                circle.Center = circle2.Center;
                circle.Radius = circle2.Radius;
            }
        }

        private static Circle FindMinimumEnclosingCircleSlow(MyPoint[] points)
        {
            Circle result = new Circle();
            int a=0, b=1;
            double distMax = 0;
            for (int i = 1; i < points.Length - 1; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (MyMath.Distance(points[i], points[j])>distMax)
                    {
                        distMax = MyMath.Distance(points[i], points[j]);
                        a = i;
                        b = j;
                    }
                }
            }
            result = new Circle(MyMath.GetMidPoint(points[a], points[b]), MyMath.Distance(points[a], points[b]) / 2);
            if(!result.AllAreInside(points))
            {
                result.Center = MyMath.CenterOfGravity(points);
                while (!result.AllAreInside(points))
                {
                    result.Radius++;
                }
            }
            return result;
            
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

        public static void ConvexHullJarvis(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint[] convexHull = GetConvexHull(points);

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawPolygon(convexHull.ToArray(), grp);
            pictureBox.Image = bmp;
        }

        private static MyPoint[] GetConvexHull(MyPoint[] points)
        {
            List<MyPoint> convexHullPoints = new List<MyPoint>();
            Array.Sort(points, (a, b) => a.X.CompareTo(b.X));
            int p = 0;
            convexHullPoints.Add(points[p]);
            do
            {

                int q = (p + 1) % n;

                for (int i = 0; i < n; i++)
                    if (MyMath.Determinant(points[p], points[q], points[i]) < 0)
                        q = i;

                convexHullPoints.Add(points[q]);
                p = q;

            } while (p != 0);

            return convexHullPoints.ToArray();
        }

        public static void RandomPolygonTriangulation(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            List<Segment> result = new List<Segment>();

            List<Segment> edges = new List<Segment>();
            for (int i = 0; i < points.Length - 1; i++)
                for (int j = i + 1; j < points.Length; j++)
                    edges.Add(new Segment(points[i], points[j]));

            edges.Sort();

            foreach(Segment edge in edges)
            {
                bool ok = true;
                foreach (Segment added in result)
                    if (edge.IntersectsForTriangulation(added))
                    {
                        ok = false;
                        break;
                    }
                if(ok)
                    result.Add(edge);
            }

            GraphicsHelper.DrawPoints(points, grp);
            GraphicsHelper.DrawSegments(result, grp);
            pictureBox.Image = bmp;
        }

        public static bool Complete = false;
        public static async Task DrawInteractivePolygon(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);
            drawnPoints.Clear();
            grp.Clear(Color.LightGray);
            DrawingEnabled = true;
            Complete = false;

            pictureBox.Image = bmp;
            while(!Complete)
            {
                await Task.Delay(100);
            }
        }

        public static void ConvexPolygonTriangulation(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] points = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint[] convexHull = GetConvexHull(points);

            for (int i = 1;i < convexHull.Length;i++)
                GraphicsHelper.DrawSegment(new Segment(convexHull[0], convexHull[i]), grp, new Pen(Color.Orange, 2));

            GraphicsHelper.DrawPolygon(convexHull, grp);
            pictureBox.Image = bmp;
        }

        public static async Task PolygonTriangulationUsingDiagonals(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            await DrawInteractivePolygon(pictureBox);
            MyPoint[] points = drawnPoints.ToArray();

            List<Segment> diagonals = new List<Segment>();

            bool finished = false;
            for (int i = 0; i < points.Length - 2 && !finished; i++)
            {
                for (int j = i + 2; j < points.Length && !finished; j++)
                {
                    if (i == 0 && j == n - 1)
                        break;

                    Segment segment = new Segment(points[i], points[j]);

                    bool ok = true;
                    foreach (Segment diagonal in diagonals)
                        if (segment.IntersectsForTriangulation(diagonal))
                            ok = false;

                    if (ok && segment.IsDiagonal(points))
                        diagonals.Add(new Segment(points[i], points[j]));

                    if (diagonals.Count == (points.Length-3))
                        finished = true;
                }
            }

            GraphicsHelper.DrawPolygon(points, grp);
            GraphicsHelper.DrawSegments(diagonals, grp, new Pen(Color.Orange, 2));

            pictureBox.Image = bmp;
        }

        private static List<Triangle> triangles = new List<Triangle>();
        public static async Task PolygonTriangulationUsingEarClipping(PictureBox pictureBox)
        {
            triangles.Clear();

            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            await DrawInteractivePolygon(pictureBox);

            List<MyPoint> points = drawnPoints;
            List<Segment> diagonals = new List<Segment>();

            GraphicsHelper.DrawPolygon(points.ToArray(), grp);

            while (points.Count > 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    int index0 = i;
                    int index1 = (i - 1 + points.Count) % points.Count;
                    int index2 = (i + 1) % points.Count;

                    Segment diagonal = new Segment(points[index1], points[index2]);

                    if(diagonal.IsDiagonal(points.ToArray()))
                    {
                        diagonals.Add(diagonal);
                        triangles.Add(new Triangle(points[index0], points[index1], points[index2]));
                        points.RemoveAt(index0);
                        break;
                    }

                }
            }
            triangles.Add(new Triangle(points[0], points[1], points[2]));

            GraphicsHelper.DrawSegments(diagonals, grp, new Pen(Color.Orange, 2));

            pictureBox.Image = bmp;
        }

        public static async Task PolygonColoringUsingEarClipping(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            await PolygonTriangulationUsingEarClipping(pictureBox);

            triangles[triangles.Count - 1].Colors[0] = Color.Red;
            triangles[triangles.Count - 1].Colors[1] = Color.Green;
            triangles[triangles.Count - 1].Colors[2] = Color.Blue;

            triangles[triangles.Count - 1].DrawTriangleColors(grp);

            for (int i = triangles.Count - 2; i >= 0; i--)
            {
                //get the colors from the last triangle
                if (triangles[i + 1].ContainsPoint(triangles[i].Points[0]) >= 0)
                    triangles[i].Colors[0] = triangles[i + 1].Colors[triangles[i + 1].ContainsPoint(triangles[i].Points[0])];
                if (triangles[i + 1].ContainsPoint(triangles[i].Points[1]) >= 0)
                    triangles[i].Colors[1] = triangles[i + 1].Colors[triangles[i + 1].ContainsPoint(triangles[i].Points[1])];
                if (triangles[i + 1].ContainsPoint(triangles[i].Points[2]) >= 0)
                    triangles[i].Colors[2] = triangles[i + 1].Colors[triangles[i + 1].ContainsPoint(triangles[i].Points[2])];

                //find the missing color if needed
                if (triangles[i].Colors[0] == Color.Black)
                    triangles[i].Colors[0] = triangles[i].GetMissingColor();
                if (triangles[i].Colors[1] == Color.Black)
                    triangles[i].Colors[1] = triangles[i].GetMissingColor();
                if (triangles[i].Colors[2] == Color.Black)
                    triangles[i].Colors[2] = triangles[i].GetMissingColor();

                triangles[i].DrawTriangleColors(grp);
            }
            pictureBox.Image = bmp;
        }

        public static void FindPolygonArea(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            MyPoint[] randomPoints = RandomGenerator.GenerateRandomPoints(n, pictureBox.Width, pictureBox.Height);
            MyPoint[] points = GetConvexHull(randomPoints);

            double polygonArea = 0;
            MyPoint origin = new MyPoint(0, 0);

            for (int i = 0; i < points.Length; i++)
            {
                polygonArea += 0.5 * MyMath.Determinant(origin, points[i], points[(i + 1) % (points.Length - 1)]);
            }

            polygonArea = Math.Abs(polygonArea);
            grp.DrawString($"Area: {polygonArea.ToString()}", new Font(FontFamily.GenericSansSerif, 15),new SolidBrush(Color.Green), new Point(10, 10));
            GraphicsHelper.DrawPolygon(points, grp);
            pictureBox.Image = bmp;
        }

        public static async Task SimplePolygonIntoMonotonePolygons(PictureBox pictureBox)
        {
            bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            grp = Graphics.FromImage(bmp);

            await DrawInteractivePolygon(pictureBox);
            MyPoint[] points = drawnPoints.ToArray();
            GraphicsHelper.DrawPolygon(points, grp);

            List<Segment> segments = new List<Segment>();

            //Array.Sort(points, (a, b) => b.Y.CompareTo(a.Y));

            for (int i = 0; i < points.Length; i++)
            {
                int indexBefore = (i - 1 + points.Length) % points.Length;
                int indexAfter = (i + 1) % points.Length;

                if (points[indexBefore].isLeftTo(points[indexAfter], points[i]))
                {
                    if (points[indexBefore].Y < points[i].Y && points[indexAfter].Y < points[i].Y)
                    {
                        for (int j = i+1; j < points.Length; j++)
                        {
                            Segment segment = new Segment(points[i], points[j]);
                            if (segment.IsDiagonal(points))
                            {
                                segments.Add(segment);
                                break;
                            }
                        }
                    }
                    if (points[indexBefore].Y > points[i].Y && points[indexAfter].Y > points[i].Y)
                    {
                        for (int j = i-1 ; j >= 0; j--)
                        {
                            Segment segment = new Segment(points[i], points[j]);
                            if (segment.IsDiagonal(points))
                            {
                                segments.Add(segment);
                                break;                            
                            }
                                
                        }
                    }
                }
            }

            GraphicsHelper.DrawSegments(segments, grp, new Pen(Color.Orange, 2));
            pictureBox.Image = bmp;
        }

        internal static void SimplePolygonIntoMonotonePolygonsLinear(PictureBox pictureBox1)
        {
            throw new NotImplementedException();
        }

        internal static void PolygonIntoConvexPolygonsLinear(PictureBox pictureBox1)
        {
            throw new NotImplementedException();
        }
    }
}
