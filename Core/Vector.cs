using System;

namespace Rendering.Core
{
    public static class Vector
    {
        public static double DotProduct(UnitVector v1, UnitVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double DotProduct(UnitVector v1, NonUnitVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double DotProduct(NonUnitVector v1, NonUnitVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static double DotProduct(NonUnitVector v1, UnitVector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static NonUnitVector CrossProduct(NonUnitVector v1, NonUnitVector v2)
        {
            return new NonUnitVector(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public static NonUnitVector CrossProduct(UnitVector v1, NonUnitVector v2)
        {
            return new NonUnitVector(
                v1.Y * v2.Z - v1.Z * v2.Y,
                v1.Z * v2.X - v1.X * v2.Z,
                v1.X * v2.Y - v1.Y * v2.X
            );
        }

        public static UnitVector Normalize(this NonUnitVector v)
        {
            var mag = v.Magnitude;

            return UnitVector.UnsafeCreateUnitVector(v.X / mag, v.Y / mag, v.Z / mag);
        }

        public static (NonUnitVector, NonUnitVector) Perpendiculars(this UnitVector v)
        {
            var vb1pre = Math.Abs(v.X) > Math.Abs(v.Y) ? UnitVector.PositiveY : UnitVector.PositiveX;
            var vb1 = vb1pre - (v * Vector.DotProduct(v, vb1pre));
            var vb2 = Vector.CrossProduct(v, vb1);

            return (vb1, vb2);
        }


        public static (UnitVector, UnitVector) NormalizedPerpendiculars(this UnitVector v)
        {
            (var p1, var p2) = v.Perpendiculars();

            return (p1.Normalize(), p2.Normalize());
        }
    }
}