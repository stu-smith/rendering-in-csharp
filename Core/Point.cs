using System;

namespace Rendering.Core
{
    public struct Point
    {
        public Point(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Point Origin => new Point(0.0, 0.0, 0.0);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public Point Translate(NonUnitVector v)
        {
            return new Point(X + v.X, Y + v.Y, Z + v.Z);
        }

        public NonUnitVector To(Point p)
        {
            return new NonUnitVector(p.X - X, p.Y - Y, p.Z - Z);
        }
    }
}