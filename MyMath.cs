﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public static class MyMath
    {
        public static double Distance(MyPoint a,MyPoint b)
        {
            int x = a.X - b.X;
            int y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }

        public static double DistanceSquared(MyPoint a, MyPoint b)
        {
            int x = a.X - b.X;
            int y = a.Y - b.Y;
            return x * x + y * y;
        }

        public static double Determinant(MyPoint a, MyPoint b, MyPoint? c)
        {
            if(c==null)
                c = new MyPoint(1,1);

            return a.X * b.Y + b.X * c.Y + a.Y * c.X - b.Y * c.X - a.Y * b.X - c.Y * a.X;
        }

        public static MyPoint GetMidPoint(MyPoint p1, MyPoint p2)
        {
            return new MyPoint((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        public static MyPoint CenterOfGravity(MyPoint[] points)
        {
            int totalX=0;
            int totalY=0;
            foreach(MyPoint point in points)
            {
                totalX += point.X;
                totalY += point.Y;
            }
            return new MyPoint(totalX/points.Length,totalY/points.Length);
        }

    }
}
