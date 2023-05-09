using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public static class RandomGenerator
    {
        private static Random rand = new Random();
        public static MyPoint GenerateRandomPoint(int minX, int maxX, int minY, int maxY)
        {
            MyPoint point = new MyPoint();

            int x = rand.Next(minX, maxX);
            int y = rand.Next(minY, maxY);
            point = new MyPoint(x, y);

            return point;
        }
        public static MyPoint[] GenerateRandomPoints(int n, int minX, int maxX, int minY, int maxY)
        {
            MyPoint[] points = new MyPoint[n];

            for (int i = 0; i < points.Length; i++)
                points[i] = GenerateRandomPoint(minX,maxX,minY,maxY);

            return points;
        }
        public static MyPoint[] GenerateRandomPoints(int n, int maxX, int maxY)
        {
            MyPoint[] points = GenerateRandomPoints(n, 0, maxX, 0, maxY);
            return points;
        }
        public static List<MyPoint> GenerateRandomPointsList(int n, int maxX, int maxY)
        {
            List<MyPoint> points = GenerateRandomPoints(n,maxX,maxY).ToList();
            return points;
        }
        public static Segment GenerateRandomSegment(int minX, int maxX, int minY, int maxY)
        {
            MyPoint a = GenerateRandomPoint(minX,maxX, minY, maxY);
            MyPoint b = GenerateRandomPoint(minX,maxX, minY, maxY);
            Segment segment = new Segment(a,b);
            return segment;
        }
        public static Segment GenerateRandomSegment(int maxX, int maxY)
        {
            return GenerateRandomSegment(0,maxX,0,maxY);
        }
        public static List<Segment> GenerateRandomSegments(int n, int minX, int maxX, int minY, int maxY)
        {
            List<Segment> segments = new List<Segment>();

            for (int i = 0; i < n; i++)
                segments.Add(GenerateRandomSegment(minX, maxX, minY, maxY));

            return segments;
        }
        public static List<Segment> GenerateRandomSegments(int n, int maxX, int maxY)
        {
            return GenerateRandomSegments(n,0,maxX,0,maxY);
        }
    }
}
