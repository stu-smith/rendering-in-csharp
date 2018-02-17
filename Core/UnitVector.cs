using System;

namespace Rendering.Core
{
    public struct UnitVector
    {
        private UnitVector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public static UnitVector PositiveX => new UnitVector(1.0, 0.0, 0.0);
        public static UnitVector NegativeX => new UnitVector(-1.0, 0.0, 0.0);

        public static UnitVector PositiveY => new UnitVector(0.0, 1.0, 0.0);
        public static UnitVector NegativeY => new UnitVector(0.0, -1.0, 0.0);

        public static UnitVector PositiveZ => new UnitVector(0.0, 0.0, 1.0);
        public static UnitVector NegativeZ => new UnitVector(0.0, 0.0, -1.0);

        public static NonUnitVector operator *(UnitVector v1, double d2)
        {
            return new NonUnitVector(v1.X * d2, v1.Y * d2, v1.Z * d2);
        }

        public static NonUnitVector operator -(UnitVector v1, NonUnitVector v2)
        {
            return new NonUnitVector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static UnitVector UnsafeCreateUnitVector(double x, double y, double z)
        {
#if DEBUG
            var ms = x * x + y * y + z * z;

            if (ms < 0.99 || ms > 1.01)
            {
                throw new InvalidOperationException($"UnsafeCreateUnitVector({x},{y}, {z}) has magnitude-squared of {ms}.");
            }
#endif

            return new UnitVector(x, y, z);
        }

        public static UnitVector UnsafeCreateUnitVector(NonUnitVector v)
        {
#if DEBUG
            var ms = v.MagnitudeSquared;

            if (ms < 0.99 || ms > 1.01)
            {
                throw new InvalidOperationException($"UnsafeCreateUnitVector({v}) has magnitude-squared of {ms}.");
            }
#endif

            return new UnitVector(v.X, v.Y, v.Z);
        }
    }
}