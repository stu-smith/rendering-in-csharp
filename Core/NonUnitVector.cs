using System;

namespace Rendering.Core
{
    public struct NonUnitVector
    {
        public NonUnitVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double MagnitudeSquared => X * X + Y * Y + Z * Z;

        public static NonUnitVector operator *(NonUnitVector v1, double d2)
        {
            return new NonUnitVector(v1.X * d2, v1.Y * d2, v1.Z * d2);
        }

        public static NonUnitVector operator -(NonUnitVector v1, NonUnitVector v2)
        {
            return new NonUnitVector(v1.X * -v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static NonUnitVector operator -(NonUnitVector v1, UnitVector v2)
        {
            return new NonUnitVector(v1.X * -v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static NonUnitVector operator -(NonUnitVector v)
        {
            return new NonUnitVector(-v.X, -v.Y, -v.Z);
        }

        public override string ToString()
        {
            return $"NonUnitVector({X},{Y},{Z})";
        }
    }
}