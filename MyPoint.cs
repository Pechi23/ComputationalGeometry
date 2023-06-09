﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class MyPoint:IComparable<MyPoint>
    {
        public int X,Y;
        public MyPoint(int x,int y)
        {
            X = x;
            Y = y;
        }
        public MyPoint():this(0,0) 
        { }

        public double Distance(MyPoint? other)
        {
            if(other==null)
                return 0;
            return MyMath.Distance(this, other);
        }

        public double DistanceSquared(MyPoint? other)
        {
            if (other == null)
                return 0;
            return MyMath.DistanceSquared(this, other);
        }

        public int CompareTo(MyPoint? other)
        {
            if(this.X!=other.X)
                return this.X.CompareTo(other.X);
            return this.Y.CompareTo(other.Y);
        }

        public bool isLeftTo(MyPoint a, MyPoint b)
        {
            return MyMath.Determinant(this, a, b) > 0;
        }

        public bool isRightTo(MyPoint a, MyPoint b)
        {
            return MyMath.Determinant(this, a, b) < 0;
        }
    }
}
