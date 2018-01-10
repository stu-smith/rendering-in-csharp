using System;

namespace Rendering.Core
{
    public sealed class Ray
    {
        public Ray(Point rayOrigin, UnitVector rayDirection)
        {
            RayOrigin = rayOrigin;
            RayDirection = rayDirection;
        }

        public Point RayOrigin { get; }
        public UnitVector RayDirection { get; }

        public Ray Translate(NonUnitVector v)
        {
            return new Ray(RayOrigin.Translate(v), RayDirection);
        }
    }
}