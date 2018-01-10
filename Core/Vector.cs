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
    }
}